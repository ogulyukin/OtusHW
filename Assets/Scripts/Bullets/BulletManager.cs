using Core;
using UnityEngine;
using UniversalComponents;

namespace Bullets
{
    public sealed class BulletManager : MonoBehaviour, IOnGameFinished
    {
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private BulletUpdateManager bulletUpdateManager;
        
        public void CreateBullet(bool isPlayer, GameObject shooter)
        {
            bulletPool.TrySpawnBullet(out var bullet);

            var weapon = shooter.GetComponent<WeaponComponent>();
            var destination = shooter.GetComponent<IFireControl>().GetFireDirection();
            
            weapon.BulletConfig.InitBullet(bullet, weapon.WeaponPosition, destination);
            bullet.IsPlayer = isPlayer;
            bulletUpdateManager.AddBulletToUpdate(bullet);
        }


        public void GameFinished()
        {
            bulletUpdateManager.StopUpdate();
        }
    }
}