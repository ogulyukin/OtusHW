using System;
using UnityEngine;

namespace Core
{
    public sealed class GameManager
    {
        public Action GameStarted;
        public Action GameFinished;
        public bool IsGamePaused { get; set; }
        public GameManager()
        {
            Debug.Log("GameManager created");
        }

        ~GameManager()
        {
            Debug.Log("GameManager destroyed");    
        }

        public void StartGame()
        {
            GameStarted?.Invoke();
        }

        public void FinishGame()
        {
            GameFinished?.Invoke();
        }

        public void PauseGame()
        {
            Time.timeScale = IsGamePaused ? 1 : 0;
            IsGamePaused = !IsGamePaused;
        }
    }
}