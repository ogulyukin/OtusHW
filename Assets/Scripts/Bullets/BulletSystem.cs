using System.Collections;
using System.Collections.Generic;
using Core;
using Level;
using UnityEngine;
using UniversalComponents;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour, IOnGameStarted, IOnGameFinished
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform container;
        [SerializeField] private float updateTimeout;
        
        private readonly Queue<Bullet> bulletPool = new();
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();
        private bool canProceed;
        
        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(bulletPrefab, container);
                bulletPool.Enqueue(bullet);
            }
        }
        
        private IEnumerator UpdateBullets()
        {
            yield return new WaitForSeconds(updateTimeout);
            if (!canProceed) yield break;
            PrepareBulletCash();

            for (int i = 0, count = cache.Count; i < count; i++)
            {
                var bullet = cache[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }

            yield return UpdateBullets();
        }

        private void PrepareBulletCash()
        {
            cache.Clear();
            cache.AddRange(activeBullets);
        }

        public void CreateBullet(bool isPlayer, GameObject shooter)
        {
            if (!canProceed)
            {
                return;
            }
            if (bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(worldTransform);
            }
            else
            {
                bullet = Instantiate(bulletPrefab, worldTransform);
            }
            
            var weapon = shooter.GetComponent<WeaponComponent>();
            var destination = shooter.GetComponent<IFireControl>().GetFireDirection();
            
            weapon.BulletConfig.InitBullet(bullet, weapon.WeaponPosition, destination);
            bullet.IsPlayer = isPlayer;
            if (activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void RemoveBullet(Bullet bullet)
        {
            if (activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(container);
                bulletPool.Enqueue(bullet);
            }
        }
        
        private void OnBulletCollision(Bullet bullet)
        {
            RemoveBullet(bullet);
        }

        public void GameStarted()
        {
            canProceed = true;
            StartCoroutine(UpdateBullets());
        }

        public void GameFinished()
        {
            canProceed = false;
            PrepareBulletCash();
            for (int i = 0, count = cache.Count; i < count; i++)
            {
                RemoveBullet(cache[i]);
            }
        }
    }
}