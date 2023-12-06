using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystemConfig : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform initTransform;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform poolStorageTransform;
        [SerializeField] private LevelBounds levelBounds;

        public int InitialCount => initialCount;

        public Transform InitTransform => initTransform;

        public Bullet BulletPrefab => bulletPrefab;

        public Transform PoolStorageTransform => poolStorageTransform;

        public LevelBounds LevelBounds => levelBounds;
    }
}
