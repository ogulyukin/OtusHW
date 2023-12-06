using System;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent
    {
        public Action OnFire;
        private float currentTime;
        private readonly float fireTimeout;
        private bool readyToFire;
        

        public EnemyAttackAgent(float timeout)
        {
            fireTimeout = timeout;
        }

        public void SetReadyToFire(bool flag)
        {
            readyToFire = flag;
        }

        public void TryAttack()
        {
            if(!readyToFire) return;
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                OnFire?.Invoke();
                currentTime += fireTimeout;
            }
        }
    }
}
