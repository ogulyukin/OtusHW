using Bullets;
using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class CharacterFireControlComponent : MonoBehaviour, IFireControl
    {
        [SerializeField] private BulletSystem bulletSystem;
        public Vector2 GetFireDirection()
        {
            return Vector2.up;
        }
        
        public void FireBullet()
        {
            bulletSystem.CreateBullet(true, gameObject);
        }
    }
}
