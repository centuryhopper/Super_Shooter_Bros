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
        public Transform player = null;
        [SerializeField] float damageAmt = 5f;

        public static HealthDamageManager instance;

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
        }

        void OnEnable()
        {
            player = GameObject.FindWithTag("Player").transform;
            health = player?.GetComponent<Health>();
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
            health?.takeDamage(damageAmt);
        }

    }
}
