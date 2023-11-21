using System.Collections;
using System.Threading;
using UnityEngine;
using UniversalComponents;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;
        public bool IsReached { get; private set; }
        public bool IsActive { get; set; }
        private Vector2 destination;

        public void StartMoveActivity()
        {
            IsActive = true;
            StartCoroutine(MoveActivity());
        }

        private IEnumerator MoveActivity()
        {
            if(!IsActive) yield break;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            Move();
            yield return MoveActivity();
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
