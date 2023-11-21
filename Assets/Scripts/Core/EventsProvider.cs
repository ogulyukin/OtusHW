using UnityEngine;

namespace Core
{
    public class EventsProvider : MonoBehaviour
    {
        private IOnGameStarted[] startGameComponents;
        private IOnGameFinished[] finishGameComponents;

        private void Awake()
        {
            startGameComponents = GetComponents<IOnGameStarted>();
            finishGameComponents = GetComponents<IOnGameFinished>();
        }

        public void StartGame()
        {
            for (var i = 0; i < startGameComponents.Length; i++)
            {
                startGameComponents[i].GameStarted();
            }
        }

        public void EndGame()
        {
            for (var i = 0; i < finishGameComponents.Length; i++)
            {
                finishGameComponents[i].GameFinished();
            }
        }
    }
}
