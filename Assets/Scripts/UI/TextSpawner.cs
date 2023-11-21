using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextSpawner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private List<string> startText;
        private const float StartTextSpawnTimeout = 1f;
        private bool isBusy;
        
        public async Task ActivateText(bool flag)
        {
            if (!flag)
            {
                gameObject.SetActive(false);
                return;
            }

            isBusy = true;
            gameObject.SetActive(true);
            StartCoroutine(StartTextCoroutine());
            while (isBusy)
            {
                await Task.Yield();
            }
        }

        private IEnumerator StartTextCoroutine()
        {
            for (int i = 0; i < startText.Count; i++)
            {
                SetNewText(startText[i]);
                yield return new WaitForSeconds(StartTextSpawnTimeout);
            }

            isBusy = false;
        }

        private void SetNewText(string newText)
        {
            text.SetText(newText);
        }
    }
}
