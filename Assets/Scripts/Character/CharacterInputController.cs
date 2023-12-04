using System;
using Core;
using Input;
using UnityEngine;
using UniversalComponents;
using Zenject;

namespace Character
{
    public sealed class CharacterInputController : IOnGameFinished, IOnGameStarted, IInitializable, IDisposable, ITickable
    {
        private readonly ComponentsStorage componentsStorage;
        private readonly CharacterFireControlComponent characterFireControlComponent;
        private readonly InputManager inputManager;
        private UserCommands lastUserCommand = UserCommands.Stop;
        private readonly GameManager gameManager;
        private bool fireRequired;

        public CharacterInputController(GameManager gManager, CharacterConfig config, CharacterFireControlComponent fireControlComponent, InputManager iManager)
        {
            gameManager = gManager;
            componentsStorage = config.GetComponent<ComponentsStorage>();
            characterFireControlComponent = fireControlComponent;
            inputManager = iManager;
            Debug.Log($"CharacterInputController created {config.gameObject.name}");
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
        public void Tick()
        {
            componentsStorage.GetMoveComponent().MoveByRigidbodyVelocity(new Vector2(UserCommandToValue(), 0) * Time.fixedDeltaTime);
            if (fireRequired)
            {
                fireRequired = false;
                characterFireControlComponent.FireBullet();
            }
        }
        
        public void GameFinished()
        {
            inputManager.OnUserCommand -= GetNewCommand;
        }

        public void GameStarted()
        {
            inputManager.OnUserCommand += GetNewCommand;
        }

        public void Initialize()
        {
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
        }

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }

    }
}
