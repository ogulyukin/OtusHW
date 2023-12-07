using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class MainMenuView : MonoBehaviour
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

        public void RemoveStartButtonListener(UnityAction action)
        {
            startButton.onClick.RemoveListener(action);
        }
        
        public void RemoveExitButtonListener(UnityAction action)
        {
            exitButton.onClick.RemoveListener(action);
        }
    }
}
