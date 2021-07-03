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
        public List<Enemy> enemyPrefabs = new List<Enemy>();
        private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();
        private WaitForSeconds waitForSeconds;
        private NavMeshTriangulation triangulation;

        void Awake()
        {
            waitForSeconds = new WaitForSeconds(spawnDelay);
            triangulation = NavMesh.CalculateTriangulation();

            // populate dictionary with all the types of enemies passed in to the list
            // each enemy prefab will have its own object pool
            for (var i = 0; i < enemyPrefabs.Count; i++)
            {
                enemyObjectPools[i] = ObjectPool.CreateInstance(enemyPrefabs[i], numEnemiesToSpawn);
            }
        }

        IEnumerator Start()
        {
            int spawnedEemies = 0;
            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                switch (enemySpawnMethod)
                {
                    case SpawnMethod.RoundRobin:
                        spawnRoundRobinEnemy(spawnedEemies);
                        break;
                    case SpawnMethod.Random:
                        spawnRandomEnemy();
                        break;
                }

                spawnedEemies++;

                yield return waitForSeconds;
            }
        }

        void spawnRoundRobinEnemy(int spawnedEnemies)
        {
            int spawnIndex = spawnedEnemies % enemyPrefabs.Count;

            spawnEnemy(spawnIndex);
        }

        void spawnRandomEnemy()
        {
            spawnEnemy(UnityEngine.Random.Range(0, enemyPrefabs.Count));

        }

        private void spawnEnemy(int spawnIndex)
        {
            PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();

            if (poolableObject is null)
            {
                UnityEngine.Debug.LogWarning($"unable to fetch enemy");
            }
            else
            {
                Enemy enemy = poolableObject as Enemy;

                int vertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);

                // -1 means all areas
                if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out NavMeshHit hit, 2f, -1))
                {
                    enemy.agent.Warp(hit.position);

                    // enable enemy NavMesh agent and start chasing player
                    enemy.aiController.player = player;
                    enemy.agent.enabled = true;
                    enemy.aiController.StartChasing();
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"Unable to place agent on navmesh. Tried to use {triangulation.vertices[vertexIndex]}");
                }
            }
        }
    }
}

