using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemyFireConfig : IFireConfig
    {
        private Transform target;
        private readonly Transform firePoint;

        public EnemyFireConfig(Transform fPoint)
        {
            firePoint = fPoint;
        }
        
        public void SetTarget(Transform player)
        {
            target = player.gameObject.transform;
        }

        public Vector2 GetFireDirection()
        {
            var startPosition = (Vector2)firePoint.position;
            var position = target.position;
            var vector = (Vector2) position - startPosition;
            return vector.normalized;
        }
    }
}
