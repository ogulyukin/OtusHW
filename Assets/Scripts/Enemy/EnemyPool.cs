using System.Collections.Generic;
using Bullets;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemyPool
    {
        private readonly EnemyPositions enemyAttackPositions;
        private readonly Transform spawnPoint;
        private readonly Transform storageTransform;
        private readonly GameObject enemyPrefab;
        private readonly int enemyQuantity;
        private readonly BulletManager bulletManager;
        private readonly UnitConfig player;
        

        private readonly Queue<GameObject> enemyPool = new();

        public EnemyPool(EnemyPositions attackPositions, Transform init, Transform storage, GameObject prefab, int quantity, BulletManager manager, UnitConfig pl)
        {
            enemyAttackPositions = attackPositions;
            spawnPoint = init;
            storageTransform = storage;
            enemyPrefab = prefab;
            enemyQuantity = quantity;
            bulletManager = manager;
            player = pl;
            EnemyPoolGeneration();
        }

        public bool TrySpawnEnemy(out GameObject enemy)
        {
            if (!enemyPool.TryDequeue(out enemy))
            {
                enemy = null;
                return false;
            }

            enemy.transform.SetParent(spawnPoint);

            var spawnPosition = enemyAttackPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = enemyAttackPositions.RandomAttackPosition();
            var enemyComponentController = enemy.GetComponent<EnemyComponentsController>();
            var enemyAgent = enemyComponentController.EnemyAgentSystem();
            enemyAgent.SetupAgent(bulletManager, attackPosition.position, true);
            enemyComponentController.SetupEnemyFireConfig(player.gameObject.transform);
            return true;
        }

        public void UnSpawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(storageTransform);
            enemy.GetComponent<EnemyComponentsController>().EnemyAgentSystem().SetMoveAgentActiveness(false);
            enemyPool.Enqueue(enemy);
        }

        private void EnemyPoolGeneration()
        {
            for (var i = 0; i < enemyQuantity; i++)
            {
                var enemy = Object.Instantiate(enemyPrefab, storageTransform);
                enemyPool.Enqueue(enemy);
            }
        }
    }
}