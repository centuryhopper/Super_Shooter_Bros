using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;

namespace Game.HealthManager
{
    // depend on the interface for who to damage and who to kill
    // interfaces are also components in Unity!!!
    public class HealthDamageManager : MonoBehaviour
    {
        [SerializeField] float playerDamageAmount = 100f;
        public float enemyDamageAmount = 1f;
        public float playerGainHealthAmount = 10f;
        public static HealthDamageManager instance;
        [SerializeField] List<GameObject> entitiesWithHealth = null;
        public GameObject player {get; private set; } = null;

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

        public void copyTransformData(Transform sourceTransform, Transform destinationTransform, Vector3 velocity)
        {
            if (sourceTransform.childCount != destinationTransform.childCount)
            {
                Debug.LogWarning("Invalid transform copy, they need to match transform hierarchies");
                return;
            }

            if (!sourceTransform.gameObject.activeInHierarchy)
            {
                // UnityEngine.Debug.LogWarning($"source transform is inactive");
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
                copyTransformData(source, destination, velocity);
            }
        }

        public bool isPlayerDead => player.GetComponent<IKillable>().isDead;

        public void resetPlayerDeath() => player.GetComponent<IKillable>().resetDeathStatus();

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

        // public void damageEnemy()
        // {
        //     foreach (var entity in entitiesWithHealth)
        //     {
        //         // TODO need to single in on the correct enemy once we have multiple enemies
        //         if (entity.name == "EnemyFighter")
        //         {
        //             entity.GetComponent<IDamageable>().takeDamage(enemyDamageAmount);
        //         }
        //     }
        // }

    }
}
