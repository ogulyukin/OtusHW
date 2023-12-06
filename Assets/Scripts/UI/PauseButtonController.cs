using System;
using Core;

namespace UI
{
    public sealed class PauseButtonController : IDisposable
    {
        private readonly GameManager gameManager;
        private readonly PauseButtonHandler pauseButtonHandler;
        
        private PauseButtonController(GameManager manager, PauseButtonHandler handler)
        {
            gameManager = manager;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            pauseButtonHandler = handler;
            pauseButtonHandler.AddPauseButtonListener(OnClickPauseButton);
            pauseButtonHandler.gameObject.SetActive(false);
        }

        private void OnClickPauseButton()
        {
            gameManager.PauseGame();
        }

        private void GameFinished()
        {
            pauseButtonHandler.gameObject.SetActive(false);
        }

        private void GameStarted()
        {
            pauseButtonHandler.gameObject.SetActive(true);
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
    }
}
