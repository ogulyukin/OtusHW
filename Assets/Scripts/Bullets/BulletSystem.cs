using System.Collections.Generic;
using Components;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private Bullet prefab;
        [SerializeField] private LevelBounds levelBounds;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cash = new();

        private void FixedUpdate()
        {
            cash.Clear();
            cash.AddRange(activeBullets);
            foreach (var bullet in cash)
            {
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBullet(bool isPlayer, GameObject shooter)
        {
            var weapon = shooter.GetComponent<Weapon>();
            var bullet = Instantiate(prefab,worldTransform);
            bullet.SetPosition(weapon.WeaponPosition);
            bullet.SetColor(weapon.BulletConfig.color);
            bullet.SetPhysicsLayer((int) weapon.BulletConfig.physicsLayer);
            bullet.Damage = weapon.BulletConfig.damage;
            bullet.IsPlayer = isPlayer;
            var destination = shooter.GetComponent<IFireControl>().GetFireDirection();
            bullet.SetVelocity(destination * weapon.BulletConfig.speed);

            if (activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += RemoveBullet;
            }
            else
            {
                Destroy(bullet);
            }
            
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= RemoveBullet;
                bullet.transform.SetParent(container);
            }
        }
    }
}