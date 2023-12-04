using UnityEngine;

namespace UniversalComponents
{
    public sealed class MoveComponent
    {
        private readonly Rigidbody2D rigidbody2D;

        private readonly float speed;

        public MoveComponent(Rigidbody2D rigidbody, float sp)
        {
            rigidbody2D = rigidbody;
            speed = sp;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 direction)
        {
            var nextPosition = rigidbody2D.position + direction * speed;
            rigidbody2D.MovePosition(nextPosition);
        }
    }
}