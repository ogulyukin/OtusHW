using UnityEngine;

namespace UniversalComponents
{
    public abstract class CustomComponentsController : MonoBehaviour
    {
        [SerializeField] private UnitConfig config;
        protected MoveComponent MovementComponent;
        private HitPointsComponent hitPointsComponent;
        protected IFireConfig FireConfig;
        

        protected virtual void Awake()
        {
            MovementComponent = new MoveComponent(config.Rigidbody2D, config.Speed);
            hitPointsComponent = new HitPointsComponent(config.HitPoints, gameObject);
        }

        public MoveComponent GetMoveComponent => MovementComponent;

        public HitPointsComponent HitPointComponent => hitPointsComponent;

        public IFireConfig GetFireConfig => FireConfig;
    }
}
