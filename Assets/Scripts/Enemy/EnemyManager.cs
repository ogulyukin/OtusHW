using System;
using Bullets;
using Core;
using UnityEngine;
using UniversalComponents;
using Zenject;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour, IDisposable
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform spawnPointTransform;
        
        [Header("Pool")]
        [SerializeField] private Transform inactiveEnemyStorageTransform;

        [SerializeField] private GameObject enemyPrefab;

        [SerializeField] private int enemyQuantity = 7;
        [SerializeField] private float spawnTimeout = 1f;

        private EnemySpawner enemySpawner;
        private EnemyPool enemyPool;
        private GameManager gameManager;
        private EnemyUpdater enemyUpdater;
        private Coroutine spawnerCoroutine;
        private BulletManager bulletManager;
        private UnitConfig player;

        [Inject]
        private void Construct(GameManager gManager, EnemyUpdater updater, BulletManager manager, UnitConfig pl)
        {
            gameManager = gManager;
            enemyUpdater = updater;
            bulletManager = manager;
            player = pl;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
        }

        private void Awake()
        {
            enemyPool = new EnemyPool(enemyPositions, spawnPointTransform, inactiveEnemyStorageTransform, enemyPrefab, enemyQuantity , bulletManager, player);
            enemySpawner = new EnemySpawner(spawnTimeout, enemyPool);
            enemyUpdater.InitEnemyUpdater(enemySpawner);
        }


        private void GameStarted()
        {
            enemySpawner.SetActivenessFlag(true);
            spawnerCoroutine = StartCoroutine(enemySpawner.SpawnEnemy());
            enemyUpdater.IsActive = true;
        }

        private void GameFinished()
        {
            enemySpawner.SetActivenessFlag(false);
            enemyUpdater.IsActive = false;
            StopCoroutine(spawnerCoroutine);
            enemySpawner.ResetEnemies();
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
        
    }
}