using System.Collections.Generic;
using Core;
using Enemy.Agents;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public sealed class EnemyUpdateHandler : MonoBehaviour
    {
        public HashSet<GameObject> ActiveEnemies { get; private set; }
        private readonly HashSet<EnemyUpdateAgent> enemyAgents = new();
        private readonly List<EnemyUpdateAgent> cache = new();

        private void Awake()
        {
            ActiveEnemies = new HashSet<GameObject>();
        }

        public void ResetHandler()
        {
            ActiveEnemies.Clear();
            enemyAgents.Clear();
        }

        public void AddHandledObjects(GameObject enemy)
        {
            ActiveEnemies.Add(enemy);
            enemyAgents.Add(enemy.GetComponent<EnemyUpdateAgent>());
        }

        public void RemoveHandledObject(GameObject enemy)
        {
            ActiveEnemies.Remove(enemy);
            enemyAgents.Remove(enemy.GetComponent<EnemyUpdateAgent>());
        }
        public void FixedUpdateMethod()
        {

        }

        private void PrepareEnemyCash()
        {
            cache.Clear();
            cache.AddRange(enemyAgents);
        }
    }
}
