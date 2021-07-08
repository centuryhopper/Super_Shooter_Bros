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
        private Coroutine CheckForLineOfSightCoroutine;

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

        private bool CheckLineOfSight(IHealable player)
        {
            Vector3 Direction = (player.getTransform().position - transform.position).normalized;
            float DotProduct = Vector3.Dot(transform.forward, Direction);
            if (DotProduct >= Mathf.Cos(FieldOfView))
            {
                if (Physics.Raycast(transform.position, Direction, out RaycastHit Hit, sphereCollider.radius, LineOfSightLayers))
                {
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

