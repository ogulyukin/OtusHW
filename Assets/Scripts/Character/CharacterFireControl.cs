using Bullets;
using Components;
using UnityEngine;

namespace Character
{
    public class CharacterFireControl : MonoBehaviour, IFireControl
    {
        [SerializeField] private BulletSystem bulletSystem;
        public Vector2 GetFireDirection()
        {
            return Vector2.up;
        }
        
        public void OnFlyBullet()
        {
            bulletSystem.FlyBullet(true, gameObject);
        }
    }
}
