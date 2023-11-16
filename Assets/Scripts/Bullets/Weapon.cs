using UnityEngine;

namespace Bullets
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig bulletConfig;
        public Vector2 WeaponPosition => firePoint.position;
        public BulletConfig BulletConfig => bulletConfig;
    }
}