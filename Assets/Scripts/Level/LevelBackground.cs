using System;
using Core;
using UnityEngine;
using Zenject;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour, IFixedTickable, IInitializable, IDisposable
    {

        [SerializeField] public float startPositionY;

        [SerializeField] public float endPositionY;

        [SerializeField] public float movingSpeedY;

        private GameManager gameManager;

        [Inject]
        private void Construct(GameManager gManager)
        {
            gameManager = gManager;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void GameStarted()
        {
            gameObject.SetActive(true);
        }

        public void GameFinished()
        {
            gameObject.SetActive(false);
        }

        public void FixedTick()
        {
            var position = transform.position;
            if (position.y <= endPositionY)
            {
                transform.position = new Vector3(position.x, startPositionY, position.z);
            }

            transform.position -= new Vector3(position.x, movingSpeedY * Time.fixedDeltaTime, position.z);
        }

        public void Initialize()
        {
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
        }

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
    }
}