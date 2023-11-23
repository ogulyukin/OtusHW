using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class TextSpawner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void ShowText()
        {
            gameObject.SetActive(true);
        }

        public void HideText()
        {
            gameObject.SetActive(false);
        }

        public void SetNewText(string newText)
        {
            text.SetText(newText);
        }
    }
}
