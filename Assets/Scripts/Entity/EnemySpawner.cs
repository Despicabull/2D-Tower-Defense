using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class EnemySpawner : NonLivingEntity
    {
        private const float TIME_BETWEEN_SPAWNS = 0.5f;
        private const float TIME_BETWEEN_WAVES = 10f;
        private const int SPAWN_PER_WAVE = 3;
        private const int WAVE_COUNT = 5;

        [SerializeField] private List<GameObject> enemyPrefabs;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        public IEnumerator SpawnEnemyWave(EnemyDespawner enemyDespawner)
        {
            for (int i = 0; i < WAVE_COUNT; i++)
            {
                for (int j = 0; j < SPAWN_PER_WAVE; j++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(TIME_BETWEEN_SPAWNS);
                }

                yield return new WaitForSeconds(TIME_BETWEEN_WAVES);
            }

            Destroy(gameObject);
            Destroy(enemyDespawner.gameObject);
        }

        public void SetPath(EnemyDespawner enemyDespawner)
        {
            // Set move path so it can give it to enemy spawned
            movePath = pathfinding.FindPath(node, enemyDespawner.node);
        }

        void SpawnEnemy()
        {
            // Get Enemy prefab from EnemyPrefabPoolManager
            // Randomize enemy spawned
            Enemy enemy = Instantiate(
                original: enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)],
                parent: transform
            ).GetComponent<Enemy>();
            enemy.movePath = movePath;
            // Spawn where enemySpawner is
            enemy.Spawn(node.x, node.y);
            enemy.Initialize(enemy.enemyDataSO);
        }
    }
}
