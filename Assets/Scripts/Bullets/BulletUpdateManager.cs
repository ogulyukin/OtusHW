using System.Collections.Generic;
using Core;
using Level;
using UnityEngine;

namespace Bullets
{
    public class BulletUpdateManager : MonoBehaviour, IOnFixedUpdate
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletPool bulletPool;
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();
        

        public void FixedUpdateMethod()
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

        public void AddBulletToUpdate(Bullet bullet)
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

        public void StopUpdate()
        {
            PrepareBulletCash();
            for (int i = 0, count = cache.Count; i < count; i++)
            {
                RemoveBullet(cache[i]);
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
    }
}
