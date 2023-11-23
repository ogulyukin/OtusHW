using Core;
using UnityEngine;

namespace UI
{
    public sealed class PauseButtonHandler : MonoBehaviour, IOnGameFinished, IOnGameStarted
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void GameFinished()
        {
            gameObject.SetActive(false);
        }

        public void GameStarted()
        {
            gameObject.SetActive(true);
        }
    }
}
