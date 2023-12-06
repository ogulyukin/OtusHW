using Bullets;
using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class CharacterFireControlComponent
    {
        private readonly BulletManager bulletManager;
        private readonly GameObject gameObject;

        public CharacterFireControlComponent(UnitConfig unit, BulletManager manager)
        {
            bulletManager = manager;
            gameObject = unit.gameObject;
        }
        
        public void FireBullet()
        {
            bulletManager.CreateBullet(true, gameObject);
        }
    }
}
