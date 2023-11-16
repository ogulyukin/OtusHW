using System.Collections.Generic;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;

        [SerializeField] private GameObject character;

        [SerializeField] private Transform worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform container;

        [SerializeField] private GameObject prefab;

        private readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(prefab, container);
                enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyFireControl>().SetTarget(character);
            return enemy;
        }

        public void UnSpawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(container);
            enemyPool.Enqueue(enemy);
        }
    }
}