using Common;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private PhysicsLayer physicsLayer;
        [SerializeField] private Color color;
        [SerializeField] private int damage;
        [SerializeField] private float speed;

        public Bullet InitBullet(Transform initPosition, Vector3 weaponPosition, Vector2 destination)
        {
            var bullet = Instantiate(bulletPrefab, initPosition);
            bullet.SetPhysicsLayer((int)physicsLayer);
            bullet.SetColor(color);
            bullet.Damage = damage;
            bullet.SetPosition(weaponPosition);
            bullet.SetVelocity(destination * speed);
            return bullet;
        }
    }
}