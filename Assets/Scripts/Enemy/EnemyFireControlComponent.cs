using Bullets;
using Enemy.Agents;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class EnemyFireControlComponent : MonoBehaviour, IFireControl
    {
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;
        private BulletSystem bulletSystem;
        private GameObject target;

        private void Start()
        {
            bulletSystem = FindObjectOfType<BulletSystem>();
        }

        private void OnEnable()
        {
            enemyAttackAgent.OnFire += OnFire;
        }

        private void OnDisable()
        {
            enemyAttackAgent.OnFire -= OnFire;
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }

        public Vector2 GetFireDirection()
        {
            var startPosition = GetComponent<WeaponComponent>().WeaponPosition;
            var position = target.transform.position;
            var vector = (Vector2) position - startPosition;
            return vector.normalized;
        }
        
        private void OnFire(GameObject enemy)
        {
            bulletSystem.CreateBullet(false, enemy);
        }
    }
}
