using System;
using Core;

namespace UI
{
    public sealed class PauseButtonController : IDisposable
    {
        private readonly GameManager gameManager;
        private readonly PauseButtonView pauseButtonView;
        
        private PauseButtonController(GameManager manager, PauseButtonView view)
        {
            gameManager = manager;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            pauseButtonView = view;
            pauseButtonView.AddPauseButtonListener(OnClickPauseButton);
            pauseButtonView.gameObject.SetActive(false);
        }

        private void OnClickPauseButton()
        {
            gameManager.PauseGame();
        }

        private void GameFinished()
        {
            pauseButtonView.gameObject.SetActive(false);
        }

        private void GameStarted()
        {
            pauseButtonView.gameObject.SetActive(true);
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
            pauseButtonView.RemovePauseButtonListener(OnClickPauseButton);
        }
    }
}
