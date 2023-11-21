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
        [SerializeField] private PhysicsLayer physicsLayer;
        [SerializeField] private Color color;
        [SerializeField] private int damage;
        [SerializeField] private float speed;

        public void InitBullet(Bullet bullet, Vector3 weaponPosition, Vector2 destination)
        {
            bullet.SetPhysicsLayer((int)physicsLayer);
            bullet.SetColor(color);
            bullet.Damage = damage;
            bullet.SetPosition(weaponPosition);
            bullet.SetVelocity(destination * speed);
        }
    }
}