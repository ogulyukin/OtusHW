using System.Collections;
using GameManager;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool enemyPool;

        [SerializeField] private EndGameManager endGameManager;

        [SerializeField] private float spawnTimeout = 1f;
        

        private bool isGameEnded;

        private IEnumerator Start()
        {
            endGameManager.OnGameOver += GameOver;
            
            while (!isGameEnded)
            {
                yield return new WaitForSeconds(spawnTimeout);
                
                
                if (enemyPool.TrySpawnEnemy(out var enemy))
                {
                    enemy.GetComponent<HitPointsComponent>().OnDeath += OnDestroyed;
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnDeath -= OnDestroyed;
            enemyPool.UnSpawnEnemy(enemy);
        }

        private void GameOver()
        {
            isGameEnded = true;
        }
        
    }
}