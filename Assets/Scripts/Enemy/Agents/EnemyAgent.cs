using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public class EnemyAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject shooter);
        
        [SerializeField] private float countdown;
        [SerializeField] private MoveComponent moveComponent;

        public event FireHandler OnFire;
        
        private float currentTime;
        private Vector2 destination;
        private bool isReached;
        
        private void FixedUpdate()
        {
            TryAttack();
            Move();
        }

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            isReached = false;
        }
        
        private void Move()
        {
            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                isReached = true;
                return;
            }
            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
        
        private void TryAttack()
        {
            if(!isReached) return;
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                Fire();
                currentTime += countdown;
            }
        }
        
        private void Fire()
        {
            OnFire?.Invoke(gameObject);
        }
    }
}
