using System.Collections;
using UnityEngine;
using Game.Interfaces;

namespace Game.EnemyAI
{
    [RequireComponent(typeof(SphereCollider))]
    public class EnemyLineOfSightChecker : MonoBehaviour
    {
        public SphereCollider sphereCollider;
        public float FieldOfView = 90f;
        public LayerMask LineOfSightLayers;
        public delegate void GainSightEvent(IHealable player);
        public GainSightEvent OnGainSight = delegate { };
        public delegate void LoseSightEvent(IHealable player);
        public LoseSightEvent OnLoseSight = delegate { };
        private Coroutine CheckForLineOfSightCoroutine = null;

        private void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // So far in my game, only the player implements the IHealable interface, so this will work
            if (other.TryGetComponent<IHealable>(out IHealable player))
            {
                if (!CheckLineOfSight(player))
                {
                    CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(player));
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"healable component not found on {other.name}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IHealable>(out IHealable player))
            {
                OnLoseSight.Invoke(player);
                if (CheckForLineOfSightCoroutine != null)
                {
                    StopCoroutine(CheckForLineOfSightCoroutine);
                }
            }
        }

        // void OnTriggerStay(Collider other)
        // {
        //     if (other.TryGetComponent<IHealable>(out IHealable player))
        //     {
        //         UnityEngine.Debug.Log($"on trigger stay");
        //         Vector3 direction = (player.getTransform().position - transform.position);
        //         // float magnitude = direction.magnitude;
        //         direction.Normalize();
        //         Debug.DrawRay(transform.position + Vector3.up, direction, Color.cyan);
        //     }
            
        // }


        private bool CheckLineOfSight(IHealable player)
        {
            Vector3 direction = (player.getTransform().position - transform.position);
            float DotProduct = Vector3.Dot(transform.forward, direction);

            if (DotProduct >= Mathf.Cos(FieldOfView))
            { 
                UnityEngine.Debug.Log($"player triggered enemy's field of view");

                // adding vector3.up to the start position really helped with raycasting since
                // the ground isn't in the way anymore
                if (Physics.Raycast(transform.position + Vector3.up, direction, out RaycastHit Hit, sphereCollider.radius, LineOfSightLayers))
                {
                    UnityEngine.Debug.Log($"TRIGGERED by raycast");
                    if (Hit.transform.GetComponent<IHealable>() != null)
                    {
                        OnGainSight.Invoke(player);
                        return true;
                    }
                }
            }

            return false;
        }

        private IEnumerator CheckForLineOfSight(IHealable player)
        {
            WaitForSeconds Wait = new WaitForSeconds(0.1f);

            while (!CheckLineOfSight(player))
            {
                // wait for a little bit so we're not checking for line of sight on every frame
                yield return Wait;
            }
        }
    }
}

