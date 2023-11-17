using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager.EndGameManager endGameManager;
        
        private void OnEnable()
        {
            character.GetComponent<HitPointsComponent>().OnDeath += OnCharacterDeath;
        }

        private void OnDisable()
        {
            if (character != null)
            {
                character.GetComponent<HitPointsComponent>().OnDeath -= OnCharacterDeath;
            }
        }
        
        private void OnCharacterDeath(GameObject _) => endGameManager.FinishGame();
        
    }
}