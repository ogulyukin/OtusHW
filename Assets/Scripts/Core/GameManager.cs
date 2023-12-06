using System;
using UnityEngine;

namespace Core
{
    public sealed class GameManager
    {
        public Action GameStarted;
        public Action GameFinished;
        private bool isGamePaused;

        public void StartGame()
        {
            GameStarted?.Invoke();
        }

        public void FinishGame()
        {
            GameFinished?.Invoke();
            Debug.Log("Game Over!");
        }

        public void PauseGame()
        {
            Time.timeScale = isGamePaused ? 1 : 0;
            isGamePaused = !isGamePaused;
        }
    }
}