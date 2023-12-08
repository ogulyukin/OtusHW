using System;
using Core;
using UnityEngine;
using Zenject;

namespace Level
{
    public sealed class LevelBackground : IFixedTickable, IDisposable
    {

        private readonly float startPositionY;

        private readonly float endPositionY;

        private readonly float movingSpeedY;

        private readonly GameManager gameManager;

        private readonly LevelBackgroundConfig levelBackgroundConfig;
        private LevelBackground(GameManager gManager, LevelBackgroundConfig config)
        {
            levelBackgroundConfig = config;
            startPositionY = config.startPositionY;
            endPositionY = config.endPositionY;
            movingSpeedY = config.movingSpeedY;
            levelBackgroundConfig.gameObject.SetActive(false);
            gameManager = gManager;
            gameManager.GameStarted += GameStarted;
            gameManager.GameFinished += GameFinished;
        }

        private void GameStarted()
        {
            levelBackgroundConfig.gameObject.SetActive(true);
        }

        private void GameFinished()
        {
            levelBackgroundConfig.gameObject.SetActive(false);
        }

        public void FixedTick()
        {
            var position = levelBackgroundConfig.transform.position;
            if (position.y <= endPositionY)
            {
                levelBackgroundConfig.transform.position = new Vector3(position.x, startPositionY, position.z);
            }

            levelBackgroundConfig.transform.position -= new Vector3(position.x, movingSpeedY * Time.fixedDeltaTime, position.z);
        }

        public void Dispose()
        {
            gameManager.GameStarted -= GameStarted;
            gameManager.GameFinished -= GameFinished;
        }
    }
}