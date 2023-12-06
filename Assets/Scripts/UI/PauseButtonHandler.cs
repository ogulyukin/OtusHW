using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;

        public void AddPauseButtonListener(UnityAction action)
        {
            pauseButton.onClick.AddListener(action);
        }
    }
}
