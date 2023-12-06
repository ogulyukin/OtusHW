using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        public void AddStartButtonListener(UnityAction action)
        {
            startButton.onClick.AddListener(action);
        }
        
        public void AddExitButtonListener(UnityAction action)
        {
            exitButton.onClick.AddListener(action);
        }
    }
}
