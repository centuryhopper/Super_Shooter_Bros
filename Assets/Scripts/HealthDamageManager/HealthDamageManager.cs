using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.PlayerCharacter;
// using Game.EnemyAI;

namespace Game.HealthManager
{
    public class HealthDamageManager : MonoBehaviour
    {
        [SerializeField] Health health = null;
        // public PlayerMovement playerMovement = null;
        public Transform player {get => health? health.transform : null;}
        [SerializeField] float damageAmt = 5f;

        public static HealthDamageManager instance;

        void Awake()
        {
            #region singleton init

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            #endregion

            // persist this manager between scenes
            DontDestroyOnLoad(gameObject);

            // playerMovement = health.GetComponent<PlayerMovement>();
        }

        public bool isPlayerDead => health.isDead;


        public void Jab()
        {
            UnityEngine.Debug.Log($"here in Jab()");
            playerTakeDamage();

            // if player is dead, disable player robot model and enable its ragdoll version
        }

        public void playerTakeDamage()
        {
            if (health != null)
            {
                health.takeDamage(damageAmt);
            }
            else
            {
                Debug.LogWarning("health is null");
            }
        }

    }
}
