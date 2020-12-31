using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.PlayerCharacter
{
    public enum AnimationParameters
    {
        move,
        jump,
        force_transition,
        isGrounded,
        turbo,
        secondJump,
        transitionIndex,
        walkDirection,

    }

    public class PlayerMovement : MonoBehaviour
    {
        BoxCollider boxCollider = null;
        public BoxCollider BoxCollider
        {
            get
            {
                return boxCollider;
            }
        }

        Rigidbody rb;
        public Rigidbody RB
        {
            get
            {
                if (rb == null)
                {
                    rb = GetComponent<Rigidbody>();
                }
                return rb;
            }
        }

        public Transform targetTransform = null;
        private Camera mainCam;


        [SerializeField] LedgeChecker ledgeChecker = null;
        public LedgeChecker GetLedgeChecker { get {return ledgeChecker;}  }
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

        void Awake()
        {
            // Debug.Log(Input.mousePresent ? "mouse detected" : "mouse not detected");
            groundCheckers = new List<GameObject>(sections + 2);
            boxCollider = GetComponent<BoxCollider>();
            rb = GetComponent<Rigidbody>();
            mainCam = Camera.main;

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

        public GameObject CreateGroundCheckingSphere(Vector3 position) => Instantiate(groundCheckingSphere, position, Quaternion.identity);


        // todo maybe migrate the code below over to a state machine script
        void Update()
        {
            // Debug.Log($"y rotation of playerskin: {playerSkin.eulerAngles.y}");

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseAimMask))
            {
                // move the target transform to where the mouse cursor is
                targetTransform.position = hit.point;
            }

            // changing the this transform's rotation had some weird inversions, so player skin turned
            // out to be the better alternative
            playerSkin.rotation = Quaternion.LookRotation(Vector3.forward * Mathf.Sign(targetTransform.position.z - this.transform.position.z), transform.up);
        }



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
    }
}

