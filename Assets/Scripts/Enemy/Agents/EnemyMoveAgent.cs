using UnityEngine;
using UniversalComponents;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;
        public bool IsReached { get; private set; }
        
        private Vector2 destination;

        private void FixedUpdate()
        {
            Move();
        }

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            IsReached = false;
        }
        
        private void Move()
        {
            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }
            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}
