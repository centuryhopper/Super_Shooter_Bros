using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.EnemyAI;
using Game.Pooling;
using UnityEngine.AI;

namespace Game.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        public enum SpawnMethod
        {
            RoundRobin,
            Random,
        }
        public SpawnMethod enemySpawnMethod = SpawnMethod.RoundRobin;
        public Transform player = null;
        public int numEnemiesToSpawn = 5;
        public float spawnDelay = 1f;
        public List<EnemyBaseStats> enemies = new List<EnemyBaseStats>();
        private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();
        private WaitForSeconds waitForSeconds;
        private NavMeshTriangulation triangulation;

        void Awake()
        {
            waitForSeconds = new WaitForSeconds(spawnDelay);
            triangulation = NavMesh.CalculateTriangulation();

            // populate dictionary with all the types of enemies passed in to the list
            // each enemy prefab will have its own object pool
            for (var i = 0; i < enemies.Count; i++)
            {
                enemyObjectPools[i] = ObjectPool.CreateInstance(enemies[i].enemyPrefab, numEnemiesToSpawn);
            }
        }

        IEnumerator Start()
        {
            int spawnedEnemies = 0;
            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                switch (enemySpawnMethod)
                {
                    case SpawnMethod.RoundRobin:
                        spawnRoundRobinEnemy(spawnedEnemies);
                        break;
                    case SpawnMethod.Random:
                        spawnRandomEnemy();
                        break;
                }

                spawnedEnemies++;

                yield return waitForSeconds;
            }
        }

        void spawnRoundRobinEnemy(int spawnedEnemies)
        {
            int spawnIndex = spawnedEnemies % enemies.Count;

            spawnEnemy(spawnIndex);
        }

        void spawnRandomEnemy()
        {
            spawnEnemy(UnityEngine.Random.Range(0, enemies.Count));

        }

        private void spawnEnemy(int spawnIndex)
        {
            PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();

            if (poolableObject is null)
            {
                UnityEngine.Debug.LogWarning($"unable to fetch enemy");
                return;
            }
            
            Enemy enemy = poolableObject as Enemy;
            enemies[spawnIndex].SetupAgentFromConfiguration(enemy);

            int vertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);

            // -1 means all areas
            if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out NavMeshHit hit, 2f, enemy.agent.areaMask))
            {
                // TODO check why warp isn't working
                if (!enemy.agent.Warp(hit.position))
                {
                    UnityEngine.Debug.LogWarning($"Agent didn't spawn successfully");
                    // return;
                }

                // enable enemy NavMesh agent and start chasing player
                enemy.aiController.player = player;
                enemy.aiController.triangulation = triangulation;
                enemy.agent.enabled = true;
                enemy.aiController.spawn();
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Unable to place agent on navmesh. Tried to use {triangulation.vertices[vertexIndex]}");
            }
        }
    }
}

