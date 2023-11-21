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

        [SerializeField] private Transform spawnPointTransform;

        [Header("Pool")]
        [SerializeField] private Transform container;

        [SerializeField] private GameObject enemyPrefab;

        [SerializeField] private int enemyQuantity = 7;

        private readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            EnemyPoolGeneration();
        }

        public bool TrySpawnEnemy(out GameObject enemy)
        {
            if (!enemyPool.TryDequeue(out enemy))
            {
                enemy = null;
                return false;
            }

            enemy.transform.SetParent(spawnPointTransform);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = enemyPositions.RandomAttackPosition();
            var enemyMoveAgent = enemy.GetComponent<EnemyMoveAgent>();
            enemyMoveAgent.SetDestination(attackPosition.position);
            enemyMoveAgent.StartMoveActivity();
            var enemyAttackAgent = enemy.GetComponent<EnemyAttackAgent>();
            enemyAttackAgent.StartOffensiveActivity();
            enemy.GetComponent<EnemyFireControlComponent>().SetTarget(character);
            return true;
        }

        public void UnSpawnEnemy(GameObject enemy)
        {
            enemy.GetComponent<EnemyMoveAgent>().IsActive = false;
            enemy.GetComponent<EnemyAttackAgent>().IsActive = false;
            enemy.transform.SetParent(container);
            enemyPool.Enqueue(enemy);
        }

        private void EnemyPoolGeneration()
        {
            for (var i = 0; i < enemyQuantity; i++)
            {
                var enemy = Instantiate(enemyPrefab, container);
                enemyPool.Enqueue(enemy);
            }
        }
        
    }
}