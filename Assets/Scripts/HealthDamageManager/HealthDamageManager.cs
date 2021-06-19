using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.PlayerCharacter;

namespace Game.HealthManager
{
    public class HealthDamageManager : MonoBehaviour
    {
        [SerializeField] GameObject playerRobot = null;
        [SerializeField] GameObject playerRobotRagdoll = null;
        [SerializeField] Health health = null;
        float damageAmt = 5f;

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
        }

        void Start()
        {
            // initially disable ragdoll
            playerRobotRagdoll.SetActive(false);
        }


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
