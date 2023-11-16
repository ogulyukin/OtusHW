using Character;
using Components;
using UnityEngine;

namespace Input
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        
        private float horizontalDirection;
        private bool fireRequired;
        private MoveComponent moveComponent;
        private CharacterFireControl characterFireControl;

        private void Start()
        {
            moveComponent = character.GetComponent<MoveComponent>();
            characterFireControl = character.GetComponent<CharacterFireControl>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                fireRequired = true;
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalDirection = -1;
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                horizontalDirection = 1;
            }
            else
            {
                horizontalDirection = 0;
            }
        }
        
        private void FixedUpdate()
        {
            moveComponent.MoveByRigidbodyVelocity(new Vector2(horizontalDirection, 0) * Time.fixedDeltaTime);
            if (fireRequired)
            {
                fireRequired = false;
                characterFireControl.OnFlyBullet();
            }
        }
    }
}