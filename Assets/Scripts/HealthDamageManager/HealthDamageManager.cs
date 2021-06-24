using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.PlayerCharacter;
// using Game.EnemyAI;
using Game.Interfaces;

namespace Game.HealthManager
{
    // depend on the interface for who to damage and who to kill
    // interfaces are also components in Unity!!!
    public class HealthDamageManager : MonoBehaviour
    {
        [SerializeField] float playerDamageAmount = 100f;
        [SerializeField] float enemyDamageAmount = 10f;
        [SerializeField] float playerGainHealthAmount = 10f;
        public static HealthDamageManager instance;
        [SerializeField] List<GameObject> entitiesWithHealth = null;

        GameObject player = null;

        void Awake()
        {
            #region singleton init

            if (instance == null)
            {
                instance = this;
            }
            // else
            // {
            //     Destroy(gameObject);
            //     return;
            // }
            #endregion

            // persist this manager between scenes
            // DontDestroyOnLoad(gameObject);
            foreach (var entity in entitiesWithHealth)
            {
                if (entity.CompareTag("Player"))
                {
                    player = entity;
                    // only one player in the game
                    break;
                }
            }
        }

        public bool isPlayerDead => player.GetComponent<IKillable>().isDead;

        /// <summary>
        /// So far this is getting called by the Enemy's provide damage class
        /// </summary>
        public void Jab()
        {
            UnityEngine.Debug.Log($"here in Jab()");
            damagePlayer();
        }

        public void playerGainHealth()
        {
            player.GetComponent<IHealable>().gainHealth(playerGainHealthAmount);
        }

        public void damagePlayer()
        {
            player.GetComponent<IDamageable>().takeDamage(playerDamageAmount);
        }

        public void damageEnemy()
        {
            foreach (var entity in entitiesWithHealth)
            {
                // TODO need to single in on the correct enemy once we have multiple enemies
                if (entity.CompareTag("EnemyFighter"))
                {
                    entity.GetComponent<IDamageable>().takeDamage(enemyDamageAmount);
                }
            }
        }

    }
}
