using Character;
using UnityEngine;

namespace UniversalComponents
{
    public class ComponentsStorage : MonoBehaviour
    {
        [SerializeField] private CharacterConfig config;
        private MoveComponent moveComponent;
        private HitPointsComponent hitPointsComponent;
        
        private void Awake()
        {
            moveComponent = new MoveComponent(config.Rigidbody2D, config.Speed);
            hitPointsComponent = new HitPointsComponent(config.HitPoints, gameObject);
        }

        public MoveComponent GetMoveComponent()
        {
            return moveComponent;
        }

        public HitPointsComponent GetHitPointComponent()
        {
            return hitPointsComponent;
        }
    }
}
