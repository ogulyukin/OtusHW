using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour, IOnGameStarted, IOnGameFinished
    {
        [SerializeField] private EnemyPool enemyPool;

        [SerializeField] private float spawnTimeout = 1f;
        

        private readonly HashSet<GameObject> activeEnemies = new();
        private bool isGameEnded;

        private Coroutine enemySpawner;

        private void OnDestroyed(GameObject enemy)
        {
            EnemyInactivation(enemy);
            activeEnemies.Remove(enemy);
            enemyPool.UnSpawnEnemy(enemy);
        }

        private void EnemyInactivation(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnDeath -= OnDestroyed;
        }


        public void GameStarted()
        {
            isGameEnded = false;
            enemySpawner = StartCoroutine(nameof(EnemySpawner));
        }
        
        private IEnumerator EnemySpawner()
        {
            while (!isGameEnded)
            {
                yield return new WaitForSeconds(spawnTimeout);
                if (enemyPool.TrySpawnEnemy(out var enemy))
                {
                    var enemyHitPointsComponent =  enemy.GetComponent<HitPointsComponent>();
                    enemyHitPointsComponent.OnDeath += OnDestroyed;
                    enemyHitPointsComponent.RestoreHitPoints();
                    activeEnemies.Add(enemy);
                }
            }
        }

        public void GameFinished()
        {
            isGameEnded = true;
            StopCoroutine(enemySpawner);
            var enemies = new List<GameObject>();
            enemies.AddRange(activeEnemies);
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                EnemyInactivation(enemies[i]);
                enemyPool.UnSpawnEnemy(enemies[i]);
            }
            activeEnemies.Clear();
        }
    }
}