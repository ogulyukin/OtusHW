using System;
using Core;
using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class PlayerStateObserver : IDisposable
    {
        private readonly GameObject player; 
        private readonly GameManager gameManager;
        
        private PlayerStateObserver(GameManager manager, UnitConfig pl)
        {
            gameManager = manager;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
            player = pl.gameObject;
            player.SetActive(false);
        }
        
        private void OnCharacterDeath(GameObject _)
        {
            gameManager.FinishGame();
            player.SetActive(false);
        }

        private void GameStarted()
        {
            var hitPointComponent = this.player.GetComponent<CustomComponentsController>().HitPointComponent;
            hitPointComponent.RestoreHitPoints();
            hitPointComponent.OnDeath += OnCharacterDeath;
        }

        private void GameFinished()
        {
            player.GetComponent<CustomComponentsController>().HitPointComponent.OnDeath -= OnCharacterDeath;
        }
        

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
    }
}