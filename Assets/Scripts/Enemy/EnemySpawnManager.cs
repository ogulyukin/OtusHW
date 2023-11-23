using System.Collections;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private float spawnTimeout = 1f;
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private EnemyUpdateHandler enemyUpdateHandler;
        private bool isGameEnded;

        private Coroutine enemySpawner;

        public void StartSpawn()
        {
            isGameEnded = false;
            enemySpawner = StartCoroutine(nameof(SpawnEnemy));
        }

        public void StopSpawn()
        {
            isGameEnded = true;
            StopCoroutine(enemySpawner);
        }

        private IEnumerator SpawnEnemy()
        {
            while (!isGameEnded)
            {
                yield return new WaitForSeconds(spawnTimeout);
                if (enemyPool.TrySpawnEnemy(out var enemy))
                {
                    var enemyHitPointsComponent =  enemy.GetComponent<HitPointsComponent>();
                    enemyHitPointsComponent.OnDeath += OnDestroyed;
                    enemyHitPointsComponent.RestoreHitPoints();
                    enemyUpdateHandler.AddHandledObjects(enemy);
                }
            }
        }
        
        private void OnDestroyed(GameObject enemy)
        {
            EnemyInactivation(enemy);
            enemyPool.UnSpawnEnemy(enemy);
            enemyUpdateHandler.RemoveHandledObject(enemy);
        }

        public void EnemyInactivation(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnDeath -= OnDestroyed;
        }
    }
}
