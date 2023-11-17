using Level;
using UnityEngine;
using UniversalComponents;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public bool IsPlayer { get; set; }
        public int Damage { get; set; }
        
        private LevelBounds levelBounds;
        
        private void FixedUpdate()
        {
            if (!levelBounds.InBounds(transform.position))
            { 
                Destroy(gameObject);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            DealDamage(collision.gameObject);
            Destroy(gameObject);
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

        public void SetLevelBounds(LevelBounds bounds)
        {
            levelBounds = bounds;
        }
        
        private void DealDamage(GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team) || IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(Damage);
            }
        }
    }
    
}