using System;
using Core;
using UnityEngine;

namespace UI
{
    public sealed class MainMenuController : IDisposable
    {
        private readonly GameManager gameManager;
        private readonly MainMenuView mainMenuView;
        private readonly GameLauncher gameLauncher;
        
        private MainMenuController(GameManager manager, MainMenuView view, GameLauncher launcher)
        {
            gameManager = manager;
            gameLauncher = launcher;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            mainMenuView = view;
            mainMenuView.AddStartButtonListener(StartGame);
            mainMenuView.AddExitButtonListener(ExitGame);
            mainMenuView.gameObject.SetActive(true);
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
            mainMenuView.gameObject.SetActive(false);
        }

        private void GameFinished()
        {
            mainMenuView.gameObject.SetActive(true);
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
            mainMenuView.RemoveStartButtonListener(StartGame);
            mainMenuView.RemoveExitButtonListener(ExitGame);
        }
    }
}
