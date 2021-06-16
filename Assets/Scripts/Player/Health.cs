using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float playerHealth = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     takeDamage(50);
        // }

        if (playerHealth == 0f)
        {
            // disable player movement
            // play death animation
            // then ask player whether to quit the game or restart
            // the level
            // we will just ask the player for now and do the other
            // two things as we progress
            Die();
        }
    }

    private void Die()
    {
        // TODO pop up a menu asking the player to either quit or restart the game
        UnityEngine.Debug.Log($"player is dead");
    }

    public void takeDamage(float damage)
    {
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
}