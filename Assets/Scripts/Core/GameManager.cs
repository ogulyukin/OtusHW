using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace Core
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private TextSpawner startTextSpawn;
        
        private bool isGamePaused;
        private EventsProvider[] gameActions;

        private void Awake()
        {
            gameActions = FindObjectsOfType<EventsProvider>();
        }

        public async Task StartGame()
        {
            Time.timeScale = 1;
            mainMenuPanel.SetActive(false);
            await startTextSpawn.ActivateText(true);
            await startTextSpawn.ActivateText(false);
            pauseButton.SetActive(true);
            for (int i = 0; i < gameActions.Length; i++)
            {
                gameActions[i].StartGame();
            }
        }
        

        public void FinishGame()
        {
            for (int i = 0; i < gameActions.Length; i++)
            {
                gameActions[i].EndGame();
            }
            Time.timeScale = 0;
            mainMenuPanel.SetActive(true);
            pauseButton.SetActive(false);
        }

        public void PauseHandler()
        {
            Time.timeScale = isGamePaused ? 1 : 0;
            isGamePaused = !isGamePaused;
        }

        public void ExitGameHandler()
        {
            Application.Quit();
        }
    }
}