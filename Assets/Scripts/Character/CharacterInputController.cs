using Input;
using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class CharacterInputController : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private CharacterFireControlComponent characterFireControlComponent;
        [SerializeField] private InputManager inputManager;
        private UserCommands lastUserCommand = UserCommands.Stop;
        private bool fireRequired;

        private void Start()
        {
            inputManager.OnUserCommand += GetNewCommand;
        }

        private void FixedUpdate()
        {
            moveComponent.MoveByRigidbodyVelocity(new Vector2(UserCommandToValue(), 0) * Time.fixedDeltaTime);
            if (fireRequired)
            {
                fireRequired = false;
                characterFireControlComponent.FireBullet();
            }
        }

        private void GetNewCommand(UserCommands command)
        {
            if (command == UserCommands.Fire)
            {
                fireRequired = true;
            }

            lastUserCommand = command;
        }

        private int UserCommandToValue()
        {
            switch (lastUserCommand)
            {
                case UserCommands.Left:
                    return -1;
                case UserCommands.Right:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
