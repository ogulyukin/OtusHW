using UnityEngine;

namespace Core
{
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameUpdateManager updateManager;
        private GameListenerComposite[] listenerComposites;
        private void Awake()
        {
           listenerComposites = FindObjectsOfType<GameListenerComposite>();
        }

        private void Start()
        {
            foreach (var composite in listenerComposites)
            {
                gameManager.OnGameStarted.AddRange(composite.StartGameComponents);
                gameManager.OnGameFinished.AddRange(composite.FinishGameComponents);
                updateManager.OnUpdate.AddRange(composite.Updates);
                updateManager.OnFixedUpdate.AddRange(composite.FixedUpdates);
            }
        }
    }
}
