using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemyUnitConfig : UnitConfig
    {
        [SerializeField] private float fireTimeout = 1f;

        public float FireRate => fireTimeout;
    }
}
