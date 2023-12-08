using UnityEngine;

namespace Enemy.Manager
{
    public sealed class EnemyPositions
    {
        private readonly Transform[] spawnPositions;

        private readonly Transform[] attackPositions;

        public EnemyPositions(Transform[] spawns, Transform[] attacks)
        {
            spawnPositions = spawns;
            attackPositions = attacks;
        }

        public Transform RandomSpawnPosition()
        {
            return RandomTransform(spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return RandomTransform(attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}