using Bullets;
using UnityEngine;

namespace UniversalComponents
{
    public class UnitConfig : MonoBehaviour
    {
        [SerializeField] private int hitPoints = 5;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Transform firePoint;
        [SerializeField] private bool isPlayer;
        
        public bool IsPlayer => isPlayer;
        public int HitPoints => hitPoints;
        public Rigidbody2D Rigidbody2D => rigidbody2D;
        public float Speed => speed;

        public BulletConfig BulletConfig => bulletConfig;

        public Transform FirePoint => firePoint;
        
    }
}
