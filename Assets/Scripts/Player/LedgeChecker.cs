using System.Collections.Generic;
using UnityEngine;
using Game.Enums;
using Game.Hash;

namespace Game.PlayerCharacter
{
    /// <summary>
    /// allows player to check for any ledges to grab onto
    /// </summary>
    public class LedgeChecker : MonoBehaviour
    {
        [HideInInspector]
        public Ledge checkLedge = null, getGrabbedLedge = null;

        public Vector3 ledgeCalibration = new Vector3();

        /// <summary>
        /// this is only ever true if player is airborne, and only collider1 is touching the ledge
        /// </summary>
        public bool isGrabbingLedge = false;
        public LedgeCollider collider1 = null, collider2 = null;
        private PlayerMovement playerMovement = null;
        private Animator playerSkinAnimator = null;
        private Shooting shooting = null;
        [SerializeField] private GameObject rifle = null;
        private Dictionary<AnimationParameters, int> tmpDict = null;

        void Awake()
        {
            playerMovement = GetComponentInParent<PlayerMovement>();


            playerSkinAnimator = playerMovement.playerSkin.GetComponent<Animator>();
            shooting = playerMovement.GetComponent<Shooting>();
        }

        void Start()
        {
            tmpDict = HashManager.Instance.animationParamsDict;
        }

        void FixedUpdate()
        {
            if (isGrabbingLedge)
            {
                // disable shooting and hide the rifle
                shooting.enabled = false;
                rifle.SetActive(false);
            }
            else
            {
                shooting.enabled = true;
                rifle.SetActive(true);
                playerMovement.RB.useGravity = true;
            }

            // if player is in the air
            if (!playerSkinAnimator.GetBool(tmpDict[AnimationParameters.isGrounded]))
            {
                foreach (GameObject o in collider1.CollidedObjects)
                {
                    if (!collider2.CollidedObjects.Contains(o))
                    {

                        if (OffSetPosition(o))
                        {
                            break;
                        }
                    }
                    else
                    {
                        isGrabbingLedge = false;
                    }
                }
            }
            else
            {
                isGrabbingLedge = false;
            }

            // not grabbing ledge if list is empty
            if (collider1.CollidedObjects.Count == 0)
            {
                isGrabbingLedge = false;
            }
        }

        bool OffSetPosition(GameObject platform)
        {
            if (isGrabbingLedge)
            {
                return true;
            }
            isGrabbingLedge = true;

            BoxCollider boxCollider = platform.GetComponent<BoxCollider>();

            if (boxCollider == null)
            {
                Debug.LogWarning($"missing box collider component from the plaform");
                return false;
            }


            playerMovement.RB.useGravity = false;
            playerMovement.RB.velocity = new Vector3(0,0,0);
            float y = platform.transform.position.y + (boxCollider.size.y / 2f);
            float z;

            if (playerMovement.IsFacingForward)
            {
                z = platform.transform.position.z - (boxCollider.size.x / 2f);
            }
            else
            {
                z = platform.transform.position.z + (boxCollider.size.x / 2f);
            }

            Vector3 platformEdge = new Vector3(0, y, z);

            if (playerMovement.IsFacingForward)
            {
                playerMovement.RB.MovePosition(platformEdge + ledgeCalibration);
            }
            else
            {
                Vector3 newLedgeCalibration = new Vector3(0, ledgeCalibration.y, -ledgeCalibration.z);
                playerMovement.RB.MovePosition(platformEdge + newLedgeCalibration);
            }


            return true;
        }



#region
        // void OnTriggerEnter(Collider other)
        // {
        //     checkLedge = other.GetComponent<Ledge>();
        //     isGrabbingLedge = checkLedge != null;
        //     getGrabbedLedge = isGrabbingLedge ? checkLedge : null;
        //     // Debug.Log($"is grabbing ledge: {isGrabbingLedge}");
        // }

        // void OnTriggerExit(Collider other)
        // {
        //     checkLedge = other.GetComponent<Ledge>();
        //     if (checkLedge != null)
        //     {
        //         isGrabbingLedge = false;
        //     }
        //     // Debug.Log($"is grabbing ledge now is: {isGrabbingLedge}");
        // }
#endregion
    }
}
