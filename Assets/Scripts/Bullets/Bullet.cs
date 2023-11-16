using System;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;

        [NonSerialized] public bool IsPlayer;
        [NonSerialized] public int Damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletUtils.DealDamage(this, collision.gameObject);
            OnCollisionEntered?.Invoke(this);    
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}