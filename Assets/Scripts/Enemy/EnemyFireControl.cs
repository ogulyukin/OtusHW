using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public class EnemyFireControl : MonoBehaviour, IFireControl
    {
        
        private BulletSystem bulletSystem;
        private GameObject target;

        private void Start()
        {
            bulletSystem = GameObject.FindWithTag("BulletSystem").GetComponent<BulletSystem>();
        }

        private void OnEnable()
        {
            GetComponent<EnemyAgent>().OnFire += OnFire;
        }

        private void OnDisable()
        {
            GetComponent<EnemyAgent>().OnFire -= OnFire;
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }

        public Vector2 GetFireDirection()
        {
            var startPosition = GetComponent<Weapon>().WeaponPosition;
            var position = target.transform.position;
            var vector = (Vector2) position - startPosition;
            return vector.normalized;
        }
        
        private void OnFire(GameObject enemy)
        {
            bulletSystem.FlyBullet(false, enemy);
        }
    }
}
