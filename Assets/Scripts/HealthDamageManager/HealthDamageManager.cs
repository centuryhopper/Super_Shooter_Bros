using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.PlayerCharacter;
using Game.singleton;

namespace Game.HealthManager
{
    public class HealthDamageManager : Singleton<HealthDamageManager>
    {
        [SerializeField] Health health;
        [SerializeField] float damageAmt = 10f;

        void Awake()
        {
            if (health == null)
            {
                health = GameObject.FindWithTag("Player").GetComponent<Health>();
            }
        }

        // jab animation event (needs monobehaviour to work)
        public void Jab()
        {
            UnityEngine.Debug.Log($"here in Jab()");
            playerTakeDamage();
        }

        public void playerTakeDamage()
        {
            if (health != null)
            {
                health.takeDamage(damageAmt);
            }
        }

        void Update()
        {
            // check the distance between each enemy in enemies and the player
            // foreach (var enemy in enemies)
            // {
            //     if (Vector3.SqrMagnitude(health.transform.position - enemy.transform.position) <= 2.5f)
            //     {
            //         health.takeDamage(damageAmt);
            //     }
            // }
        }
    }
}
