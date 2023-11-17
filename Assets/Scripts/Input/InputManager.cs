using System;
using UnityEngine;

namespace Input
{
    public enum UserCommands
    {
        Left,
        Right,
        Stop,
        Fire
    }
    public sealed class InputManager : MonoBehaviour
    {
        public Action<UserCommands> OnUserCommand;
        
        private void Update()
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