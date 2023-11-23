using Core;
using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour, IOnGameStarted, IOnGameFinished
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        
        private void Start()
        {
            character.SetActive(false);
        }
        

        private void OnCharacterDeath(GameObject _)
        {
            gameManager.FinishGame();
            character.SetActive(false);
        }

        public void GameStarted()
        {
            character.GetComponent<HitPointsComponent>().RestoreHitPoints();
            character.GetComponent<HitPointsComponent>().OnDeath += OnCharacterDeath;
        }

        public void GameFinished()
        {
            character.GetComponent<HitPointsComponent>().OnDeath -= OnCharacterDeath;
        }
    }
}