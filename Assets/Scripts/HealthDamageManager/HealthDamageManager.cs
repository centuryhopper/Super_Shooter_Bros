using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.EnemyAI;

public class HealthDamageManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] EnemyMovement[] enemies;
    [SerializeField] float damageAmt = 10f;

    void Start()
    {
        if (health == null)
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }
        if (enemies == null)
        {
            enemies = FindObjectsOfType<EnemyMovement>();
        }
    }

    void Update()
    {
        // check the distance between each enemy in enemies and the player
        foreach (var enemy in enemies)
        {
            if (Vector3.SqrMagnitude(health.transform.position - enemy.transform.position) <= 2.5f)
            {
                health.takeDamage(damageAmt);
            }
        }
    }
}
