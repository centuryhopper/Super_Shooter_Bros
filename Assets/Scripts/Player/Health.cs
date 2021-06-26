using System;
using UnityEngine;
using Game.Interfaces;
using Game.HealthManager;

namespace Game.PlayerCharacter
{
    public class Health : MonoBehaviour, IDamageable, IHealable, IKillable
    {
        public float playerHealth = 100f;
        public bool isDead { get; set;} = false;
        [SerializeField] GameObject playerRobot = null;
        [SerializeField] GameObject playerRobotRagdoll = null;
        PlayerMovement playerMovement = null;
        Rigidbody rb = null;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
            // initially disable ragdoll
            playerRobotRagdoll.SetActive(false);
        }

        public void stopAllPlayerMovement()
        {
            UnityEngine.Debug.Log($"stopping all player movement");
            playerMovement.allowMovement = false;
            playerMovement.jump = false;
            playerMovement.moveUp = false;
            playerMovement.moveDown = false;
            playerMovement.moveLeft = false;
            playerMovement.moveRight = false;
        }

        void Update()
        {
            if (playerHealth == 0f)
            {
                // disable player movement
                // play death animation
                // then ask player whether to quit the game or restart
                // the level
                // we will just ask the player for now and do the other
                // two things as we progress
                // Invoke("restartGameDelegate", 0f);
                die();
                handleDeath();
            }
        }

        public void handleDeath()
        {
            HealthDamageManager.instance.copyTransformData(playerRobot.transform, playerRobotRagdoll.transform, rb.velocity);
            rb.velocity = Vector3.zero;

            // turn on ragdoll and turn off player robot mesh
            playerRobot.SetActive(false);
            playerRobotRagdoll.SetActive(true);

            stopAllPlayerMovement();
            playerMovement.enabled = false;
            playerMovement.GetComponent<Shooting>().enabled = false;
        }

        public void die()
        {
            // TODO pop up a menu asking the player to either quit or restart the game
            UnityEngine.Debug.Log($"player is dead");
            isDead = true;
        }

        public void takeDamage(float damage)
        {
            UnityEngine.Debug.Log($"player taking damage");
            playerHealth -= damage;
            if (playerHealth < 0f)
            {
                playerHealth = 0f;
            }
        }

        public void gainHealth(float health)
        {
            playerHealth += health;
            if (playerHealth > 100f)
            {
                playerHealth = 100f;
            }
        }

        public void resetDeathStatus()
        {
            this.isDead = false;
        }
    }
}
