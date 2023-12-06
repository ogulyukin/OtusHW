using System;
using UnityEngine;
using Zenject;

namespace Input
{
    public enum UserCommands
    {
        Left,
        Right,
        Stop,
        Fire
    }
    public sealed class InputManager : ITickable
    {
        public Action<UserCommands> OnUserCommand;

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OnUserCommand?.Invoke(UserCommands.Fire);
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                OnUserCommand?.Invoke(UserCommands.Left);
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                OnUserCommand?.Invoke(UserCommands.Right);
            }
            else
            {
                OnUserCommand?.Invoke(UserCommands.Stop);
            }
        }
    }
}