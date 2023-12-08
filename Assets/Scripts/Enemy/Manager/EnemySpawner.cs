using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UniversalComponents;

namespace Enemy.Manager
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

        public async Task SpawnEnemy(CancellationToken token)
        {
            while (isGameStarted)
            {
                await Task.Delay((int)spawnTimeout * 1000, token);
                if (enemyPool.TrySpawnEnemy(out var enemy))
                {
                    var enemyHitPointsComponent = enemy.GetComponent<CustomComponentsController>().HitPointComponent;
                    enemyHitPointsComponent.OnDeath += OnDestroyed;
                    enemyHitPointsComponent.RestoreHitPoints();
                    activeEnemies.Add(enemy);
                }

                if (token.IsCancellationRequested)
                {
                    Debug.Log("Task Cancelled");
                    return;    
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
