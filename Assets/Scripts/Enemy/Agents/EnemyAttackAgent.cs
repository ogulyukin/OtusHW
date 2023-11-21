using System;
using System.Collections;
using UnityEngine;

namespace Enemy.Agents
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float fireTimeout = 3;
        [SerializeField] private EnemyMoveAgent moveAgent;
        public Action<GameObject> OnFire;
        private float currentTime;
        private const float FireRate = 0.5f;
        public bool IsActive { get; set; }

        public void StartOffensiveActivity()
        {
            IsActive = true;
            StartCoroutine(OffensiveActivity());
        }
        
        public IEnumerator OffensiveActivity()
        {
            if(!IsActive) yield break;
            yield return new WaitForSeconds(FireRate);
            TryAttack();
            yield return OffensiveActivity();
        }

        private void TryAttack()
        {
            if(!moveAgent.IsReached) return;
            currentTime -= FireRate;
            if (currentTime <= 0)
            {
                OnFire?.Invoke(gameObject);
                currentTime += fireTimeout;
            }
        }
    }
}
