using Enemy.Agents;
using UnityEngine;
using UniversalComponents;

namespace Enemy
{
    public sealed class EnemyComponentsController : CustomComponentsController
    {
        private EnemyFireConfig enemyFireConfig;
        private EnemyAgentsSystem agentsSystem;
        protected override void Awake()
        {
            base.Awake();
            var tr = transform;
            enemyFireConfig = new EnemyFireConfig(tr);
            FireConfig = enemyFireConfig;
            agentsSystem = new EnemyAgentsSystem(MovementComponent, tr, GetComponent<EnemyUnitConfig>().FireRate);
        }

        public void SetupEnemyFireConfig(Transform player)
        {
            enemyFireConfig.SetTarget(player);
        }

        public EnemyAgentsSystem EnemyAgentSystem() => agentsSystem;
    }
}
