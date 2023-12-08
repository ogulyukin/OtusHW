using UnityEngine;

namespace Enemy.Manager
{
    public class EnemyManagerConfig : MonoBehaviour
    {
        [Header("Spawn")]
        //[SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform spawnPointTransform;
        
        [Header("Pool")]
        [SerializeField] private Transform inactiveEnemyStorageTransform;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemyQuantity = 7;
        [SerializeField] private float spawnTimeout = 1f;
        
        [Header("Spawn / Attack Positions")]
        [SerializeField] private Transform[] spawnPositions;
        [SerializeField] private Transform[] attackPositions;
        
        
        public Transform SpawnPointTransform => spawnPointTransform;

        public Transform InactiveEnemyStorageTransform => inactiveEnemyStorageTransform;

        public GameObject EnemyPrefab => enemyPrefab;

        public int EnemyQuantity => enemyQuantity;

        public float SpawnTimeout => spawnTimeout;

        public Transform[] SpawnPositions => spawnPositions;

        public Transform[] AttackPositions => attackPositions;
    }
}
