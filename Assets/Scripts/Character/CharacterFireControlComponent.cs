using Bullets;
using UnityEngine;

namespace Character
{
    public sealed class CharacterFireControlComponent
    {
        private readonly BulletManager bulletManager;
        private readonly GameObject gameObject;

        public CharacterFireControlComponent(CharacterConfig character, BulletManager manager)
        {
            bulletManager = manager;
            gameObject = character.gameObject;
        }
        
        public void FireBullet()
        {
            bulletManager.CreateBullet(true, gameObject);
        }
    }
}
