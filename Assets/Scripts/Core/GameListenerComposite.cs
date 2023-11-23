using UnityEngine;

namespace Core
{
    public sealed class GameListenerComposite : MonoBehaviour
    {
        public IOnGameStarted[] StartGameComponents { get; private set; }
        public IOnGameFinished[] FinishGameComponents { get; private set; }
        public IOnFixedUpdate[] FixedUpdates { get; private set; }
        public IOnUpdate[] Updates { get; private set; }

        private void Awake()
        {
            StartGameComponents = GetComponents<IOnGameStarted>();
            FinishGameComponents = GetComponents<IOnGameFinished>();
            FixedUpdates = GetComponents<IOnFixedUpdate>();
            Updates = GetComponents<IOnUpdate>();
        }
    }
}
