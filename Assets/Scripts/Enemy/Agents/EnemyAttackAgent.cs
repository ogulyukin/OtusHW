using System;
using UnityEngine;

namespace Enemy.Agents
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float fireTimeout = 3;
        [SerializeField] private EnemyMoveAgent moveAgent;
        public Action<GameObject> OnFire;
        private float currentTime;
        
        private void FixedUpdate()
        {
            TryAttack();
        }
        
        private void TryAttack()
        {
            if(!moveAgent.IsReached) return;
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                OnFire?.Invoke(gameObject);
                currentTime += fireTimeout;
            }
        }
    }
}
