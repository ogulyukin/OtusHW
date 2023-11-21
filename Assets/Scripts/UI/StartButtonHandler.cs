using Core;
using UnityEngine;

namespace UI
{
    public class StartButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        
        public void OnStartButtonClick()
        {
            var _ = gameManager.StartGame();
        }
    }
}
