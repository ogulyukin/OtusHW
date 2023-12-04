using System;
using UnityEngine;

namespace UniversalComponents
{
    public sealed class HitPointsComponent
    {
        public event Action<GameObject> OnDeath;
        private readonly int hitPoints;
        private int currentHitPoints;
        private readonly GameObject gameObject;

        public HitPointsComponent(int points, GameObject gObject)
        {
            hitPoints = points;
            gameObject = gObject;
        }
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