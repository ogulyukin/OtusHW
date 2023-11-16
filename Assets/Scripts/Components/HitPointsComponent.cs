using System;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> HpEmpty;
        
        [SerializeField] private int hitPoints;
        
        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                HpEmpty?.Invoke(gameObject);
            }
        }
    }
}