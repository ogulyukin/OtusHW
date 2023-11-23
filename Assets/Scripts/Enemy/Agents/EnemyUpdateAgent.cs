using Core;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyUpdateAgent : MonoBehaviour, IOnFixedUpdate
    {
        [SerializeField] private EnemyAttackAgent attackAgent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        public void FixedUpdateMethod()
        {
            attackAgent.TryAttack();
            moveAgent.Move();
        }
    }
}
