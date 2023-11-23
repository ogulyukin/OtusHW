using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace Core
{
    public sealed class GameLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private TextSpawner startTextSpawn;
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private List<string> startText;
        private const float StartTextSpawnTimeout = 1f;
        private bool isBusy;
        
        public async Task LaunchGame()
        {
            mainMenuPanel.SetActive(false);
            startTextSpawn.ShowText();
            await ActivateText();
            startTextSpawn.HideText();
            pauseButton.SetActive(true);
            character.SetActive(true);
            gameManager.StartGame();
        }
        private async Task ActivateText()
        {
            isBusy = true;
            for (int i = 0; i < startText.Count; i++)
            {
                StartCoroutine(ShowNewText());
            }
            while (isBusy)
            {
                await Task.Yield();
            }
        }

        private IEnumerator ShowNewText()
        {
            foreach (var text in startText)
            {
                startTextSpawn.SetNewText(text);
                yield return new WaitForSeconds(StartTextSpawnTimeout);
            }

            isBusy = false;
        }
    }
}
