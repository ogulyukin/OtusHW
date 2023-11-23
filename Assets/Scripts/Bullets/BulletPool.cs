using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletPool : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform container;

        private readonly Queue<Bullet> bulletPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(bulletPrefab, container);
                bulletPool.Enqueue(bullet);
            }
        }

        public void TrySpawnBullet(out Bullet bullet)
        {
            if (!bulletPool.TryDequeue(out bullet))
            {
                bullet = Instantiate(bulletPrefab, worldTransform);
            }

            bullet.transform.SetParent(worldTransform);
        }

        public void UnSpawnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(container);
            bullet.SetVelocity(Vector2.zero);
            bulletPool.Enqueue(bullet);
        }
    }
}
