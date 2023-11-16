using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager.GameManager gameManager;
        
        private void OnEnable()
        {
            character.GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            character.GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }
        
        private void OnCharacterDeath(GameObject _) => gameManager.FinishGame();
        
    }
}