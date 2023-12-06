using System;
using Core;
using UnityEngine;

namespace UI
{
    public sealed class MainMenuController : IDisposable
    {
        private readonly GameManager gameManager;
        private readonly MainMenuHandler mainMenuHandler;
        private readonly GameLauncher gameLauncher;
        
        private MainMenuController(GameManager manager, MainMenuHandler handler, GameLauncher launcher)
        {
            gameManager = manager;
            gameLauncher = launcher;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            mainMenuHandler = handler;
            mainMenuHandler.AddStartButtonListener(StartGame);
            mainMenuHandler.AddExitButtonListener(ExitGame);
            mainMenuHandler.gameObject.SetActive(true);
        }

        private void StartGame()
        {
            _ = gameLauncher.LaunchGame();
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void GameStarted()
        {
            mainMenuHandler.gameObject.SetActive(false);
        }

        private void GameFinished()
        {
            mainMenuHandler.gameObject.SetActive(true);
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
    }
}
