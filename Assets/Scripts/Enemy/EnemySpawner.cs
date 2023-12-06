using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemySpawner
    {
        private readonly float spawnTimeout;
        private readonly EnemyPool enemyPool;
        private bool isGameStarted;
        private readonly HashSet<GameObject> activeEnemies = new();

        public EnemySpawner(float timeout, EnemyPool pool)
        {
            spawnTimeout = timeout;
            enemyPool = pool;
        }
        
        public void SetActivenessFlag(bool flag)
        {
            isGameStarted = flag;
        }

        public IEnumerator SpawnEnemy()
        {
            while (isGameStarted)
            {
                yield return new WaitForSeconds(spawnTimeout);
                if (enemyPool.TrySpawnEnemy(out var enemy))
                {
                    var enemyHitPointsComponent = enemy.GetComponent<CustomComponentsController>().HitPointComponent;
                    enemyHitPointsComponent.OnDeath += OnDestroyed;
                    enemyHitPointsComponent.RestoreHitPoints();
                    activeEnemies.Add(enemy);
                }
            }
        }

        public void ResetEnemies()
        {
            var enemies = GetListOfActiveEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyInactivation(enemies[i]);
                enemyPool.UnSpawnEnemy(enemies[i]);
            }
        }

        public List<GameObject> GetListOfActiveEnemies()
        {
            var enemies = new List<GameObject>();
            enemies.AddRange(activeEnemies);
            return enemies;
        }

        private void OnDestroyed(GameObject enemy)
        {
            EnemyInactivation(enemy);
            enemyPool.UnSpawnEnemy(enemy);
        }

        private void EnemyInactivation(GameObject enemy)
        {
            enemy.GetComponent<CustomComponentsController>().HitPointComponent.OnDeath -= OnDestroyed;
            activeEnemies.Remove(enemy);
        }
    }
}
