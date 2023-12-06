using System;
using Core;
using Input;
using UnityEngine;
using UniversalComponents;
using Zenject;

namespace Character
{
    public sealed class CharacterInputController : IDisposable, ITickable
    {
        private readonly CustomComponentsController customComponentsController;
        private readonly CharacterFireControlComponent characterFireControlComponent;
        private readonly InputManager inputManager;
        private UserCommands lastUserCommand = UserCommands.Stop;
        private readonly GameManager gameManager;
        private bool fireRequired;
        private bool isGameStarted;

        public CharacterInputController(GameManager gManager, UnitConfig config, CharacterFireControlComponent fireControlComponent, InputManager iManager)
        {
            gameManager = gManager;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            customComponentsController = config.GetComponent<CustomComponentsController>();
            characterFireControlComponent = fireControlComponent;
            inputManager = iManager;
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
            if(!isGameStarted)
                return;
            customComponentsController.GetMoveComponent.MoveByRigidbodyVelocity(new Vector2(UserCommandToValue(), 0) * Time.fixedDeltaTime);
            if (fireRequired)
            {
                fireRequired = false;
                characterFireControlComponent.FireBullet();
            }
        }

        private void GameFinished()
        {
            inputManager.OnUserCommand -= GetNewCommand;
            isGameStarted = false;
        }

        private void GameStarted()
        {
            inputManager.OnUserCommand += GetNewCommand;
            isGameStarted = true;
        }

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
            inputManager.OnUserCommand -= GetNewCommand;
        }

    }
}
