using System;
using System.Collections.Generic;
using Core;
using Level;
using UnityEngine;
using UniversalComponents;
using Zenject;

namespace Bullets
{
    public sealed class BulletManager : IFixedTickable, IDisposable
    {
        private readonly BulletPool bulletPool;
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();
        private readonly LevelBounds levelBounds;
        private readonly GameManager gameManager;

        public BulletManager(BulletSystemConfig config, GameManager gManager)
        {
            bulletPool = new BulletPool(config.InitialCount, config.InitTransform, config.BulletPrefab, config.PoolStorageTransform);
            levelBounds = config.LevelBounds;
            gameManager = gManager;
            gameManager.GameFinished += GameFinished;
        }

        public void CreateBullet(bool isPlayer, GameObject shooter)
        {
            bulletPool.TrySpawnBullet(out var bullet);

            var unitConfig = shooter.GetComponent<UnitConfig>();
            var destination = shooter.GetComponent<CustomComponentsController>().GetFireConfig.GetFireDirection();
            
            unitConfig.BulletConfig.InitBullet(bullet, unitConfig.FirePoint.position, destination);
            bullet.IsPlayer = isPlayer;
            AddBulletToUpdate(bullet);
        }

        private void AddBulletToUpdate(Bullet bullet)
        {
            activeBullets.Add(bullet);
            bullet.OnCollisionEntered += OnBulletCollision;
        }
        
        private void RemoveBullet(Bullet bullet)
        {
            if (activeBullets.Remove(bullet))
            {
                bulletPool.UnSpawnBullet(bullet);
                bullet.OnCollisionEntered -= OnBulletCollision;
            }
        }

        private void PrepareBulletCash()
        {
            cache.Clear();
            cache.AddRange(activeBullets);
        }

        private void OnBulletCollision(Bullet bullet)
        {
            RemoveBullet(bullet);
        }
        
        public void FixedTick()
        {
            PrepareBulletCash();

            for (int i = 0, count = cache.Count; i < count; i++)
            {
                var bullet = cache[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        private void GameFinished()
        {
            PrepareBulletCash();
            for (int i = 0, count = cache.Count; i < count; i++)
            {
                RemoveBullet(cache[i]);
            }
        }
        

        public void Dispose()
        {
            gameManager.GameFinished -= GameFinished;
        }
    }
}