using Zenject;

namespace Enemy.Manager
{
    public sealed class EnemyUpdater : IFixedTickable
    {
        private EnemySpawner spawner;
        public bool IsActive { get; set; }

        public void InitEnemyUpdater(EnemySpawner eSpawner)
        {
            spawner = eSpawner;
        }

        public void FixedTick()
        {
            if(!IsActive) return;
            var enemiesToUpdate = spawner.GetListOfActiveEnemies();
            for (int i = 0; i < enemiesToUpdate.Count; i++)
            {
                enemiesToUpdate[i].GetComponent<EnemyComponentsController>().EnemyAgentSystem().CustomFixTick();
            }
        }
    }
}
