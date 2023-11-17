using System;
using UnityEngine;

namespace UniversalComponents
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;
        
        [SerializeField] private int hitPoints;
        
        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                OnDeath?.Invoke(gameObject);
            }
        }
    }
}