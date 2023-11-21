using System;
using UnityEngine;

namespace UniversalComponents
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;
        
        [SerializeField] private int hitPoints;
        private int currentHitPoints;

        public void TakeDamage(int damage)
        {
            currentHitPoints -= damage;
            if (currentHitPoints <= 0)
            {
                OnDeath?.Invoke(gameObject);
            }
        }

        public void RestoreHitPoints()
        {
            currentHitPoints = hitPoints;
        }
    }
}