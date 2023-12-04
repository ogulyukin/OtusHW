using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour, IOnGameStarted, IOnGameFinished
    {
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private EnemyUpdateHandler enemyUpdateHandler;
        [SerializeField] private EnemySpawnManager enemySpawnManager;


        public void GameStarted()
        {
            //enemySpawnManager.StartSpawn();
        }

        public void GameFinished()
        {
            enemySpawnManager.StopSpawn();
            var enemies = new List<GameObject>();
            enemies.AddRange(enemyUpdateHandler.ActiveEnemies);
            for (int i = 0; i < enemyUpdateHandler.ActiveEnemies.Count; i++)
            {
                enemySpawnManager.EnemyInactivation(enemies[i]);
                enemyPool.UnSpawnEnemy(enemies[i]);
            }
            enemyUpdateHandler.ResetHandler();
        }
    }
}