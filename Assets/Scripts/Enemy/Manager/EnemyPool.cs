using System.Collections.Generic;
using Bullets;
using UnityEngine;
using UniversalComponents;

namespace Enemy.Manager
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

        public EnemyPool(EnemyManagerConfig config, BulletManager manager, UnitConfig pl)
        {
            enemyAttackPositions = new EnemyPositions(config.SpawnPositions, config.AttackPositions);
            spawnPoint = config.SpawnPointTransform;
            storageTransform = config.InactiveEnemyStorageTransform;
            enemyPrefab = config.EnemyPrefab;
            enemyQuantity = config.EnemyQuantity;
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