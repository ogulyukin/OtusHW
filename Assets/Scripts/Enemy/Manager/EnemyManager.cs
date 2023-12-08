using System;
using System.Threading;
using Bullets;
using Core;
using UniversalComponents;

namespace Enemy.Manager
{
    public sealed class EnemyManager : IDisposable
    {
        private readonly EnemySpawner enemySpawner;
        private readonly GameManager gameManager;
        private readonly EnemyUpdater enemyUpdater;
        private CancellationTokenSource spawnerTaskCancellationSource;

        private EnemyManager(EnemyManagerConfig config, GameManager gManager, EnemyUpdater updater, BulletManager manager, UnitConfig pl)
        {
            gameManager = gManager;
            enemyUpdater = updater;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            var enemyPool = new EnemyPool(config , manager, pl);
            enemySpawner = new EnemySpawner(config.SpawnTimeout, enemyPool);
            enemyUpdater.InitEnemyUpdater(enemySpawner);
        }


        private void GameStarted()
        {
            enemySpawner.SetActivenessFlag(true);
            enemyUpdater.IsActive = true;
            spawnerTaskCancellationSource = new CancellationTokenSource();
            CancellationToken spawnerCancellationToken = spawnerTaskCancellationSource.Token;
            var _ = enemySpawner.SpawnEnemy(spawnerCancellationToken);
        }

        private void GameFinished()
        {
            enemySpawner.SetActivenessFlag(false);
            enemyUpdater.IsActive = false;
            spawnerTaskCancellationSource.Cancel();
            enemySpawner.ResetEnemies();
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
        
    }
}