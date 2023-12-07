using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseButtonView : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;

        public void AddPauseButtonListener(UnityAction action)
        {
            pauseButton.onClick.AddListener(action);
        }
        
        public void RemovePauseButtonListener(UnityAction action)
        {
            pauseButton.onClick.RemoveListener(action);
        }
    }
}
