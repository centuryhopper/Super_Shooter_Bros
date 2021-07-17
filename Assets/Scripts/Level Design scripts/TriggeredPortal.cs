using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.portal_manager;
using Game.Spawner;
using Game.EnemyAI;


namespace Game.LevelDesign
{
    public class TriggeredPortal : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            // only call this when all enemies in the scene are deactivated
            if (Enemy.enemyDeaths == EnemySpawner.totalNumEnemies)
            {
                PortalManager.instance.loadDesiredScene();
            }
        }
    }
}
