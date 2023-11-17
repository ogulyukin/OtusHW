using System;
using UnityEngine;

namespace GameManager
{
    public sealed class EndGameManager : MonoBehaviour
    {
        public event Action OnGameOver;
        public void FinishGame()
        {
            OnGameOver?.Invoke();
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}