using Level;
using UnityEngine;
using UniversalComponents;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        
        public void CreateBullet(bool isPlayer, GameObject shooter)
        {
            var weapon = shooter.GetComponent<WeaponComponent>();
            var destination = shooter.GetComponent<IFireControl>().GetFireDirection();
            var bullet = weapon.BulletConfig.InitBullet(worldTransform, weapon.WeaponPosition, destination);
            bullet.IsPlayer = isPlayer;
            bullet.SetLevelBounds(levelBounds);
        }
    }
}