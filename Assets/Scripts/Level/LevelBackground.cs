using System.Collections;
using Core;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour, IOnGameStarted, IOnGameFinished
    {

        [SerializeField] public float startPositionY;

        [SerializeField] public float endPositionY;

        [SerializeField] public float movingSpeedY;

        [SerializeField] public float backgroundMoveTimeout = 0.1f;

        private bool proceedBackgroundMove;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator UpdateBackground()
        {
            yield return new WaitForSeconds(backgroundMoveTimeout);
            if(!proceedBackgroundMove) yield break;
            var position = transform.position;
            if (position.y <= endPositionY)
            {
                transform.position = new Vector3(position.x, startPositionY, position.z);
            }

            transform.position -= new Vector3(position.x, movingSpeedY * Time.fixedDeltaTime, position.z);

            yield return UpdateBackground();
        }

        public void GameStarted()
        {
            proceedBackgroundMove = true;
            gameObject.SetActive(true);
            StartCoroutine(UpdateBackground());
        }

        public void GameFinished()
        {
            proceedBackgroundMove = false;
            gameObject.SetActive(false);
        }
    }
}