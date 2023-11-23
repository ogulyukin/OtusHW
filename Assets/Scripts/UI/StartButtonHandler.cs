using Core;
using UnityEngine;

namespace UI
{
    public sealed class StartButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameLauncher gameLauncher;
        
        public void OnStartButtonClick()
        {
            var _ =gameLauncher.LaunchGame();
        }
    }
}
