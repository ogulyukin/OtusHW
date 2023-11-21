using Core;
using UnityEngine;

namespace UI
{
    public sealed class StartButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        public void OnStartButtonClick()
        {
            var _ = gameManager.StartGame();
        }
    }
}
