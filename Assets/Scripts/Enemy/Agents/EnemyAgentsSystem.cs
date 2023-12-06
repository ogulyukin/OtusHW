using Bullets;
using UnityEngine;
using UniversalComponents;

namespace Enemy.Agents
{
    public sealed class EnemyAgentsSystem
    {
        private readonly EnemyAttackAgent attackAgent;
        private readonly EnemyMoveAgent moveAgent;
        private BulletManager bulletManager;
        private readonly Transform enemyTransform;
        
        public EnemyAgentsSystem(MoveComponent mComponent, Transform transform, float fireTimeout)
        {
            attackAgent = new EnemyAttackAgent(fireTimeout);
            moveAgent = new EnemyMoveAgent(mComponent, transform);
            attackAgent.OnFire += OnFire;
            moveAgent.OnDestinationReached += DestinationReached;
            enemyTransform = transform;
        }

        ~EnemyAgentsSystem()
        {
            attackAgent.OnFire -= OnFire;
            moveAgent.OnDestinationReached -= DestinationReached;
        }

        public void CustomFixTick()
        {
            moveAgent.TryMove();
            attackAgent.TryAttack();
        }
        public void SetMoveAgentActiveness(bool flag)
        {
            moveAgent.ActivateActiveness(flag);
        }

        private void OnFire()
        {
            bulletManager.CreateBullet(false, enemyTransform.gameObject);
        }

        private void DestinationReached(bool reached)
        {
            attackAgent.SetReadyToFire(reached);
        }

        public void SetupAgent(BulletManager manager, Vector3 endPoint, bool flag)
        {
            bulletManager = manager;
            moveAgent.SetDestination(endPoint);
            moveAgent.ActivateActiveness(flag);
        }
    }
}
