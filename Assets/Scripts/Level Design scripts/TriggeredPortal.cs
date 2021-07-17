using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.portal_manager;
using Game.Spawner;


namespace Game.LevelDesign
{
    public class TriggeredPortal : MonoBehaviour
    {

        [SerializeField] EnemySpawner enemySpawner = null;

        void OnTriggerEnter(Collider other)
        {
            // only call this when all enemies in the scene are deactivated
            // if (enemySpawner.numEnemiesToSpawn)
            
            PortalManager.instance.loadDesiredScene();
        }
    }
}
