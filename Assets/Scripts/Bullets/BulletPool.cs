using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletPool
    {
        private readonly Transform initTransform;
        private readonly Bullet bulletPrefab;
        private readonly Transform poolStorageTransform;

        private readonly Queue<Bullet> bulletPool = new();

        public BulletPool(int initialCount, Transform iTransform, Bullet bPrefab, Transform storageTransform)
        {
            initTransform = iTransform;
            poolStorageTransform = storageTransform;
            bulletPrefab = bPrefab;
            
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Object.Instantiate(bulletPrefab, poolStorageTransform);
                bulletPool.Enqueue(bullet);
            }
        }

        public void TrySpawnBullet(out Bullet bullet)
        {
            if (!bulletPool.TryDequeue(out bullet))
            {
                bullet = Object.Instantiate(bulletPrefab, initTransform);
            }

            bullet.transform.SetParent(initTransform);
        }

        public void UnSpawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(poolStorageTransform);
            bullet.SetVelocity(Vector2.zero);
            bulletPool.Enqueue(bullet);
        }
    }
}
