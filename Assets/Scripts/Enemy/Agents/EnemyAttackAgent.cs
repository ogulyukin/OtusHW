using System;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float fireTimeout = 3;
        [SerializeField] private EnemyMoveAgent moveAgent;
        public Action<GameObject> OnFire;
        private float currentTime;

        public void TryAttack()
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
