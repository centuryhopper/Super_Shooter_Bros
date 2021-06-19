using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Animations.Rigging;
using Game.Inputs;
using Game.Hash;
using Game.Enums;
using Game.GenericCharacter;
using Game.Interfaces;


namespace Game.PlayerCharacter
{

    public class PlayerMovement : MonoBehaviour
    {
        [HideInInspector]
        public Animator skinnedMeshAnimator = null;
        public AnimationProgress animationProgress;
        public BoxCollider boxCollider = null;
        public BoxCollider BoxCollider { get => boxCollider; }
        Rigidbody rb;
        public Rigidbody RB { get => rb; }
        public Transform crossHairTransform = null;
        private Camera mainCam;
        [SerializeField] LedgeChecker ledgeChecker = null;
        public LedgeChecker GetLedgeChecker { get => ledgeChecker; }
        public Transform playerSkin = null;
        public CollisionSpheres collisionSpheres = null;

        /// <summary>
        /// With this, player now has double jump ability
        /// </summary>
        [HideInInspector]
        public static int numJumps = 2;
        public LayerMask mouseAimMask;

        [ReadOnly]
        public float faceDirection = 1;
        public bool IsFacingForward => faceDirection == 1;

        /// <summary>
        /// includes the 3 rig layers I have childed to the skin mesh
        /// the weapon aim rig layer is not included because I have a
        /// separate variable refrence that takes care of that
        /// </summary>
        public List<Rig> rigs = null;
        public Rig weaponAimRig = null;
        public GameObject rifle = null;
        [SerializeField] float tolerableDistance = 2.5f;

        public bool jump;
        public bool moveLeft;
        public bool moveRight;
        public bool moveUp;
        public bool moveDown;

        /// <summary>
        /// Is true when player holds down
        /// shift or when player is "run" mode
        /// </summary>
        /// <value></value>
        public bool turbo;
        public bool secondJump;

        [Header("Gravity")]
        public ContactPoint[] contactPoints;

        #region Messed around with animation curves
        // [SerializeField] AnimationCurve sinPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve cosPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve tanPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve aSinPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve aCosPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve aTanPlot = new AnimationCurve();

        // [SerializeField] AnimationCurve cscPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve secPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve cotPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve aCscPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve aSecPlot = new AnimationCurve();
        // [SerializeField] AnimationCurve aCotPlot = new AnimationCurve();
        // aSinPlot.AddKey(Time.realtimeSinceStartup, Mathf.Asin(Time.time));
        // aCosPlot.AddKey(Time.realtimeSinceStartup, Mathf.Acos(Time.time));
        // aTanPlot.AddKey(Time.realtimeSinceStartup, Mathf.Atan(Time.time));

        // cscPlot.AddKey(Time.realtimeSinceStartup, 1 / Mathf.Sin(Time.time));
        // secPlot.AddKey(Time.realtimeSinceStartup, 1 / Mathf.Cos(Time.time));
        // cotPlot.AddKey(Time.realtimeSinceStartup, 1 / Mathf.Tan(Time.time));
        #endregion

        Transform t;


        void Awake()
        {
            // Debug.Log(Input.mousePresent ? "mouse detected" : "mouse not detected");

            collisionSpheres = GetComponentInChildren<CollisionSpheres>();
            skinnedMeshAnimator = playerSkin.GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider>();
            rb = GetComponent<Rigidbody>();
            t = transform;
            mainCam = Camera.main;
            collisionSpheres.owner = this;
            // Debug.Log($"player parent: {transform.parent is null}");
        }

        void Start()
        {
            collisionSpheres.SetColliderSpheres();
        }

        public void ToggleRigLayerWeights(int weightNumber)
        {
            Debug.Log($"setting rig weights to {weightNumber}");
            // rigs.ForEach(rig => rig.weight = weightNumber);
            rigs[1].weight = weightNumber;
            rigs[2].weight = weightNumber;
            weaponAimRig.weight = weightNumber;
        }

        public AnimationStateNames GetCurrentAnimatorStateName(Dictionary<int, AnimationStateNames> hashedIntStateNamePairs)
        {
            // get the current animator state info
            AnimatorStateInfo stateInfo = skinnedMeshAnimator.GetCurrentAnimatorStateInfo(0);

            if (hashedIntStateNamePairs.TryGetValue(stateInfo.shortNameHash, out AnimationStateNames stateName))
            {
                return stateName;
            }
            else
            {
                Debug.LogWarning("Unknown animator state name.");
                return default(AnimationStateNames);
            }
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            contactPoints = collisionInfo.contacts;
        }

        // public void CreateMiddleSpheres(GameObject start, Vector3 direction, float section, int sections, List<GameObject> spheresList)
        // {
        //     for (int i = 0; i < sections; ++i)
        //     {
        //         // find position for each section
        //         //         X       X       X       X       X
        //         // | ..... | ..... | ..... | ..... | ..... | ..... |
        //         Vector3 position = start.transform.position + (direction * section * (i + 1));

        //         // instantiate sphere
        //         GameObject sphere = CreateGroundCheckingSphere(position);

        //         // parent it to the player
        //         sphere.transform.parent = this.transform;

        //         // add it to the list
        //         spheresList.Add(sphere);
        //     }
        // }

        public void UpdateBoxColliderSize()
        {
            if (!animationProgress.isUpdatingBoxCollider)
            {
                return;
            }

            // updating the boxcollider size only when significant difference is found

            if (Vector3.SqrMagnitude(boxCollider.size - animationProgress.targetSize) > 0.01f)
            {
                boxCollider.size = Vector3.Lerp(boxCollider.size,
                    animationProgress.targetSize,
                    Time.deltaTime * animationProgress.sizeSpeed);

                animationProgress.isUpdatingSpheres = true;
            }
        }

        public void UpdateBoxColliderCenter()
        {
            if (!animationProgress.isUpdatingBoxCollider)
            {
                return;
            }

            if (Vector3.SqrMagnitude(boxCollider.center - animationProgress.targetCenter) > 0.01f)
            {
                boxCollider.center = Vector3.Lerp(boxCollider.center,
                    animationProgress.targetCenter,
                    Time.deltaTime * animationProgress.centerSpeed);

                animationProgress.isUpdatingSpheres = true;
            }
        }

        // void FixedUpdate()
        // {
        // todo add a gravity multiplier

        // }

        // todo maybe migrate the code below over to a state machine script
        void Update()
        {
            // Debug.Log($"top, bottom, front, back: {GetTopBottomFrontBackDimensions()}");

            // Debug.Log(GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict));

            // todo potential bug in this switch statement
            switch (GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict))
            {
                case AnimationStateNames.HangingIdle:
                case AnimationStateNames.ForwardRoll:
                    ToggleRigLayerWeights(0);
                    break;
                case AnimationStateNames.Idle:
                case AnimationStateNames.Walk:
                    ToggleRigLayerWeights(1);
                    break;
                default:
                    Debug.Log($"invalid state name");
                    break;
            }


            animationProgress.isUpdatingSpheres = false;
            UpdateBoxColliderSize();
            UpdateBoxColliderCenter();

            if (animationProgress.isUpdatingSpheres)
            {
                collisionSpheres.RepositionBottomSpheres();
                collisionSpheres.RepositionFrontSpheres();
                collisionSpheres.RepositionBackSpheres();
            }

            // plauyer aim will follow the crosshair transform, which follows the hit.point,
            // if the hit.point is far enough
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);

            // methods to keep in mind for rig weight control
            // if the ray hit something and the the point was far enough from the player
            // OR
            // toggle animation rig weights depending on the distance of the crosshair
            // to the player
            if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, mouseAimMask) && !ledgeChecker.isGrabbingLedge)
            {
                // Debug.Log($"ray hit something");
                // Vector3 fromPlayerToCrossHair = crossHairTransform.position - transform.position;
                Vector3 fromHitPointToCrossHair = hit.point - t.position;
                // plot.AddKey(Time.realtimeSinceStartup, fromHitPointToCrossHair.sqrMagnitude);

                // accurately compares the distance and
                // saves a sqrt call on every frame that
                // this code is reached
                if (fromHitPointToCrossHair.sqrMagnitude >= tolerableDistance * tolerableDistance /*distance is far enough*/ )
                {
                    // Debug.Log($"far enough with a distance of {fromPlayerToCrossHair.sqrMagnitude}");
                    // Debug.Log($"far enough with a distance of {fromHitPointToCrossHair.sqrMagnitude}");

                    // todo temporarily commented the line below out
                    // weaponAimRig.weight = 1;

                    // move the target transform to where the mouse cursor is
                    // but because we're looking at the player on a y-z plane, we should only ever
                    // change those two coordinates
                    float x = crossHairTransform.position.x;
                    float y = hit.point.y;
                    float z = hit.point.z;
                    Vector3 newCrossHairPosition = new Vector3(x, y, z);
                    crossHairTransform.position = newCrossHairPosition;
                }
                else
                {
                    // Debug.Log($"too close with a distance of {fromPlayerToCrossHair.sqrMagnitude}");
                    // Debug.Log($"too close with a distance of {fromHitPointToCrossHair.sqrMagnitude}");
                    // weaponAimRig.weight = 0;
                }
            }

            // if (GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict) != AnimationStateNames.HangingIdle &&
            //     GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict) != AnimationStateNames.LedgeClimb)
            //     {
            //         transform.rotation = Quaternion.LookRotation(Vector3.forward * Mathf.Sign(crossHairTransform.position.z - t.position.z), transform.up);
            //     }

            if (GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict) == AnimationStateNames.Idle ||
                GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict) == AnimationStateNames.Jump_Normal ||
                GetCurrentAnimatorStateName(HashManager.Instance.stateNamesDict) == AnimationStateNames.Walk)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward * Mathf.Sign(crossHairTransform.position.z - t.position.z), transform.up);
            }

            // hover your mouse cursor over this function call for comment details
            faceDirection = DotProductWithComments(transform.forward, Vector3.forward);

            #region Keyboardinput syncs
            jump = VirtualInputManager.Instance.jump;
            moveLeft = VirtualInputManager.Instance.moveLeft;
            moveRight = VirtualInputManager.Instance.moveRight;
            moveUp = VirtualInputManager.Instance.moveUp;
            moveDown = VirtualInputManager.Instance.moveDown;
            turbo = VirtualInputManager.Instance.turbo;
            secondJump = VirtualInputManager.Instance.secondJump;
            #endregion
        }





        /// <summary>
        /// Returns...<br/>
        /// 1 if both vectors are facing the same direction with each other.<br/>
        /// -1 if both vectors are facing the opposite direction.<br/>
        /// 0 if both vectors are perpendicular with each other.
        /// </summary>
        private float DotProductWithComments(Vector3 l, Vector3 r) => Vector3.Dot(l, r);

        #region old code
        // This method can't be called if this script and the animator component aren't
        // attached to the same game object
        // void OnAnimatorIK()
        // {
        //     // Weapon aim at target ik

        //     // position sets for ik goals
        //     animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
        //     animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);
        //     animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);
        //     animator.SetIKPosition(AvatarIKGoal.LeftHand, targetTransform.position);
        // }
        #endregion

        #region
        // // debug code
        // void OnCollisionEnter(Collision collisionInfo)
        // {
        //     print(collisionInfo.gameObject.name);
        // }

        // /// <summary>
        // /// moves the player left and right
        // /// </summary>
        // void MovePlayer()
        // {
        //     // side scroller
        //     print("here");
        //     if (Input.GetKey(KeyCode.D))
        //     {
        //         transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //         // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //         transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);
        //         anim.SetBool(AnimationParameters.move.ToString(), true);
        //     }
        //     else if (Input.GetKey(KeyCode.A))
        //     {
        //         transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //         // transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //         transform.rotation = Quaternion.LookRotation(-Vector3.forward, transform.up);
        //         anim.SetBool(AnimationParameters.move.ToString(), true);
        //     }
        //     else
        //     {
        //         anim.SetBool(AnimationParameters.move.ToString(), false);
        //     }
        // }

        // void FlipSprite()
        // {
        //     // don't cache Input.GetAxis("Horizontal"), or it will not work
        //     if (Input.GetAxis("Horizontal") != 0)
        //     {
        //         float zScale = Mathf.Abs(transform.localScale.z);
        //         transform.localScale = (Input.GetAxis("Horizontal") > 0) ?
        //         new Vector3(transform.localScale.x,transform.localScale.y,zScale) :
        //         new Vector3(transform.localScale.x,transform.localScale.y,-zScale);
        //     }
        // }
        #endregion
    }
}

