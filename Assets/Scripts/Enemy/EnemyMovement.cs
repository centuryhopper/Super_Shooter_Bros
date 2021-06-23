using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Interfaces;
using NaughtyAttributes;

namespace Game.EnemyAI
{
    public class EnemyMovement : MonoBehaviour, IControllable
    {
        [HideInInspector]
        public AIProgress aiProgress;

        // there should be 13 ragDollParts available
        // CharacterJoint[] characterJoints;
        [SerializeField] private GameObject ragDoll;
        [SerializeField] private GameObject animatedModel;
        // [SerializeField] private NavMeshAgent navmeshAgent;

        // [Header("AI movements")]
        private bool isDead;

        // ai movements
        public bool canAttack { get; set; }
        public bool jump { get; set; }
        public bool moveLeft { get; set; }
        public bool moveRight { get; set; }
        public bool moveUp { get; set; }
        public bool moveDown { get; set; }
        public bool turbo { get; set; }
        public bool secondJump { get; set; }

        [SerializeField] private float speed = 3f;

        [ReadOnly]
        public float faceDirection = 1;
        public bool IsFacingForward => faceDirection == 1;
        public Rigidbody rb = null;
        // public bool hasReachedStartOffMesh = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            aiProgress = GetComponentInChildren<AIProgress>();
            ragDoll.gameObject.SetActive(false);
        }

        private void Update()
        {
            faceDirection = Vector3.Dot(transform.forward, Vector3.forward);

            // for debugging
            if (Input.GetKeyDown(KeyCode.O))
            {
                ToggleDead();
            }

            #region physical movement logic
            // TODO set up movment physics here
            if (moveRight)
            {
                // make sure the enemy is facing right when walking right
                if (!IsFacingForward)
                {
                    FaceForward(true);
                }

                UnityEngine.Debug.Log($"Moving right");

                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else if (moveLeft)
            {
                // make sure the enemy is facing left when walking left
                if (IsFacingForward)
                {
                    FaceForward(false);
                }

                UnityEngine.Debug.Log($"Moving left");

                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }


            #endregion
        }


        // TODO use this for player death as well
        [ContextMenu("ToggleDead")]
        private void ToggleDead()
        {
            isDead = !isDead;

            if (isDead)
            {
                CopyTransformData(animatedModel.transform, ragDoll.transform, rb.velocity);
                ragDoll.gameObject.SetActive(true);
                animatedModel.gameObject.SetActive(false);
                // navmeshAgent.enabled = false;
            }
            else
            {
                // Switch back to the model and disable the ragdoll
                ragDoll.gameObject.SetActive(false);
                animatedModel.gameObject.SetActive(true);
                // navmeshAgent.enabled = true;
            }

        }

        private void CopyTransformData(Transform sourceTransform, Transform destinationTransform, Vector3 velocity)
        {
            if (sourceTransform.childCount != destinationTransform.childCount)
            {
                Debug.LogWarning("Invalid transform copy, they need to match transform hierarchies");
                return;
            }

            for (int i = 0; i < sourceTransform.childCount; i++)
            {
                Transform source = sourceTransform.GetChild(i);
                Transform destination = destinationTransform.GetChild(i);
                destination.position = source.position;
                destination.rotation = source.rotation;
                Rigidbody rb = destination.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.velocity = velocity;

                // recursive call on children
                CopyTransformData(source, destination, velocity);
            }
        }

        public void FaceForward(bool shouldFaceForward)
        {
            if (shouldFaceForward)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
        }

    }
}
