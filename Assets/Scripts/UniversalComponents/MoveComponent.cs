using UnityEngine;

namespace UniversalComponents
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;

        [SerializeField] private float speed = 5.0f;
        
        public void MoveByRigidbodyVelocity(Vector2 direction)
        {
            var nextPosition = rigidbody2D.position + direction * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}