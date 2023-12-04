using UnityEngine;
using Zenject;

namespace Enemy.Agents
{
    public sealed class EnemyUpdateAgent : MonoBehaviour, IFixedTickable
    {
        [SerializeField] private EnemyAttackAgent attackAgent;
        [SerializeField] private EnemyMoveAgent moveAgent;

        public void FixedTick()
        {
            attackAgent.TryAttack();
            moveAgent.Move();
        }
    }
}
