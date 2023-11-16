using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool enemyPool;

        private readonly HashSet<GameObject> activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                var enemy = enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().HpEmpty += OnDestroyed;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().HpEmpty -= OnDestroyed;
                enemyPool.UnSpawnEnemy(enemy);
            }
        }
        
    }
}