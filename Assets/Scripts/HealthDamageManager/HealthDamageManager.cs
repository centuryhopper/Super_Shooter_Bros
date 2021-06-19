using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.PlayerCharacter;
using Game.singleton;

namespace Game.HealthManager
{
    public class HealthDamageManager : Singleton<HealthDamageManager>
    {
        Health health;
        float damageAmt = 5f;


        public void Jab()
        {
            UnityEngine.Debug.Log($"here in Jab()");
            playerTakeDamage();
        }

        public void playerTakeDamage()
        {
            if (health == null)
            {
                health = GameObject.FindWithTag("Player").GetComponent<Health>();
            }

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
