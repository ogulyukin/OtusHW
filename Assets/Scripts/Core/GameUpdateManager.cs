using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class GameUpdateManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        public List<IOnFixedUpdate> OnFixedUpdate { get; private set; }
        public List<IOnUpdate> OnUpdate { get; private set; }

        private void Awake()
        {
            OnUpdate = new List<IOnUpdate>();
            OnFixedUpdate = new List<IOnFixedUpdate>();
        }

        private void Update()
        {
            if (CheckUpdateConditions()) return;
            for(var i = 0; i < OnUpdate.Count; i++)
            {
                OnUpdate[i].UpdateMethod();
            }
        }
        
        private void FixedUpdate()
        {
            if (CheckUpdateConditions()) return;
            for(var i = 0; i < OnFixedUpdate.Count; i++)
            {
                OnFixedUpdate[i].FixedUpdateMethod();
            }
        }

        private bool CheckUpdateConditions()
        {
            if (!gameManager.isGameStarted || gameManager.isGamePaused)
            {
                return true;
            }

            return false;
        }
    }
}
