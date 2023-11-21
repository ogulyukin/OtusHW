using System;
using System.Collections;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private float fireTimeout = 3;
        [SerializeField] private EnemyMoveAgent moveAgent;
        public Action<GameObject> OnFire;
        private float currentTime;
        public bool IsActive { get; set; }

        public void StartOffensiveActivity()
        {
            IsActive = true;
            StartCoroutine(OffensiveActivity());
        }

        private IEnumerator OffensiveActivity()
        {
            if(!IsActive) yield break;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            TryAttack();
            yield return OffensiveActivity();
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
