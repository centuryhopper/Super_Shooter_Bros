using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Animations.Rigging;

namespace Game.PlayerCharacter
{

    // [ExecuteInEditMode]
    public class PlayerMovement : MonoBehaviour
    {
        public AnimationProgress animationProgress;
        BoxCollider boxCollider = null;
        public BoxCollider BoxCollider
        {
            get => boxCollider;
        }

        Rigidbody rb;
        public Rigidbody RB
        {
            get => rb;
        }

        public Transform crossHairTransform = null;
        private Camera mainCam;
        private Animator animator;


        [SerializeField] LedgeChecker ledgeChecker = null;
        public LedgeChecker GetLedgeChecker { get {return ledgeChecker;} }
        [SerializeField] Transform playerSkin = null;
        public Transform PlayerSkin { get {return playerSkin;} set { playerSkin = value; } }
        public List<GameObject> groundCheckers { get; private set; }
        [SerializeField] GameObject groundCheckingSphere = null;
        [SerializeField] int sections = 5;

        /// <summary>
        /// With this, player now has double jump ability
        /// </summary>
        [HideInInspector]
        public static int numJumps = 2;
        public LayerMask mouseAimMask;

        [ReadOnly]
        public float faceDirection = 1;
        public bool IsFacingForward => faceDirection == 1;

        [SerializeField] List<Rig> rigs = null;
        [SerializeField] Rig weaponAimRig = null;
        [SerializeField] float tolerableDistance = 2.5f;

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
            groundCheckers = new List<GameObject>(sections + 2);
            boxCollider = GetComponent<BoxCollider>();
            rb = GetComponent<Rigidbody>();
            t = transform;
            mainCam = Camera.main;
            animator = GetComponent<Animator>();


            #region groundchecking spheres
                // y-z plane in this case
                float top = boxCollider.bounds.center.y + boxCollider.bounds.extents.y;
                float bottom = boxCollider.bounds.center.y - boxCollider.bounds.extents.y;

                float front = boxCollider.bounds.center.z + boxCollider.bounds.extents.z;
                float back = boxCollider.bounds.center.z - boxCollider.bounds.extents.z;

                // create the spheres and add them to the list
                GameObject bottomFrontSphere = CreateGroundCheckingSphere(new Vector3(0, bottom, front));
                GameObject bottomBackSphere = CreateGroundCheckingSphere(new Vector3(0, bottom, back));

                groundCheckers.Add(bottomFrontSphere);
                groundCheckers.Add(bottomBackSphere);

                // parent the player to the spheres so the positions of the ground checkers are accurate
                bottomFrontSphere.transform.parent = this.transform;
                bottomBackSphere.transform.parent = this.transform;

                // divide into 5 sections and add a sphere for each section
                float section = (bottomFrontSphere.transform.position
                - bottomBackSphere.transform.position).magnitude / sections;

                for (int i = 0; i < sections; ++i)
                {
                    // find position for each section
                    //         X       X       X       X       X
                    // | ..... | ..... | ..... | ..... | ..... | ..... |
                    Vector3 position = bottomBackSphere.transform.position + (Vector3.forward * section * (i + 1));

                    // instantiate sphere
                    GameObject sphere = CreateGroundCheckingSphere(position);

                    // parent it to the player
                    sphere.transform.parent = this.transform;

                    // add it to the list
                    groundCheckers.Add(sphere);
                }

            #endregion

        }

        private void OnCollisionEnter(Collision collectible)
        {
            //Debug.Log("Collision Detected");
            if(collectible.gameObject.CompareTag("Collectible"))
            {
                Debug.Log("triggered collectible");
                PlayerStats.Points++;
                PlayerStats.updateStats();
                Item item = collectible.gameObject.GetComponent<ItemPickup>().item;
                Debug.Log("picking up " + item.name);
                Inventory.instance.Add(item);
                Destroy(collectible.gameObject);
                
            }
        
        }

        public GameObject CreateGroundCheckingSphere(Vector3 position) => Instantiate(groundCheckingSphere, position, Quaternion.identity);


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
                    Time.fixedDeltaTime * animationProgress.sizeSpeed);
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
                    Time.fixedDeltaTime * animationProgress.centerSpeed);
            }
        }

        void FixedUpdate()
        {
            // todo add a gravity multiplier

            UpdateBoxColliderSize();
            UpdateBoxColliderCenter();
        }

        // todo maybe migrate the code below over to a state machine script
        void Update()
        {

            if (ledgeChecker.isGrabbingLedge)
            {
                rigs.ForEach(rig => rig.weight = 0);
                weaponAimRig.weight = 0;
                return;
            }
            else
            {
                rigs.ForEach(rig => rig.weight = 1);
            }

            // Debug.Log($"y rotation of playerskin: {playerSkin.eulerAngles.y}");

            // plauyer aim will follow the crosshair transform, which follows the hit.point,
            // if the hit.point is far enough
            Ray mouseRay = mainCam.ScreenPointToRay(Input.mousePosition);

            // methods to keep in mind for rig weight control
            // if the ray hit something and the the point was far enough from the player
            // OR
            // toggle animation rig weights depending on the distance of the crosshair
            // to the player
            if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, mouseAimMask))
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
                    weaponAimRig.weight = 1;

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
                    weaponAimRig.weight = 0;
                }
            }

            if (!ledgeChecker.isGrabbingLedge)
                transform.rotation = Quaternion.LookRotation(Vector3.forward * Mathf.Sign(crossHairTransform.position.z - t.position.z), transform.up);

            // hover your mouse cursor over this function call for comment details
            faceDirection = DotProductWithComments(transform.forward, Vector3.forward);
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

