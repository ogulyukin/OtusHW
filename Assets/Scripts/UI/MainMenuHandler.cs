using Core;
using UnityEngine;

namespace UI
{
    public sealed class MainMenuHandler : MonoBehaviour, IOnGameStarted, IOnGameFinished
    {
        public void GameStarted()
        {
            gameObject.SetActive(false);
        }

        public void GameFinished()
        {
            gameObject.SetActive(true);
        }
    }
}
