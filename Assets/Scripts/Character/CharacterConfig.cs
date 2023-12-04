using Bullets;
using UnityEngine;
using UniversalComponents;

namespace Character
{
    public class CharacterConfig : MonoBehaviour
    {
        [SerializeField] private int hitPoints = 5;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private Transform firePoint;
        public int HitPoints => hitPoints;
        public Rigidbody2D Rigidbody2D => rigidbody2D;
        public float Speed => speed;

        public BulletConfig BulletConfig => bulletConfig;

        public Transform FirePoint => firePoint;

        private MoveComponent moveComponent;
    }
}
