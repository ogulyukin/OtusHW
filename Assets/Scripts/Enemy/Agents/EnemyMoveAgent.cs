using System;
using UnityEngine;
using UniversalComponents;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent
    {
        public Action<bool> OnDestinationReached;
        private readonly MoveComponent moveComponent;
        private readonly Transform transform;
        private Vector2 destination;
        private bool isActive;

        public EnemyMoveAgent(MoveComponent mComponent, Transform tr)
        {
            transform = tr;
            moveComponent = mComponent;
        }

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            OnDestinationReached?.Invoke(false);
        }

        public void ActivateActiveness(bool flag)
        {
            isActive = flag;
        }

        public void TryMove()
        {
            if(!isActive) return;
            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                OnDestinationReached?.Invoke(true);
                isActive = false;
                return;
            }
            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}
