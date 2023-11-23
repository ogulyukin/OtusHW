using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public List<IOnGameStarted> OnGameStarted { get; private set; }
        public List<IOnGameFinished> OnGameFinished { get; private set; }

        public bool isGamePaused;
        public bool isGameStarted;
        private void Awake()
        {
            OnGameFinished = new List<IOnGameFinished>();
            OnGameStarted = new List<IOnGameStarted>();
        }

        public void StartGame()
        {
            foreach (var gameStart in OnGameStarted)
            {
                gameStart.GameStarted();
            }
            isGameStarted = true;
        }

        public void FinishGame()
        {
            foreach (var gameFinished in OnGameFinished)
            {
                gameFinished.GameFinished();
            }
            isGameStarted = false;
        }

        public void PauseGame()
        {
            Time.timeScale = isGamePaused ? 1 : 0;
            isGamePaused = !isGamePaused;
        }
    }
}