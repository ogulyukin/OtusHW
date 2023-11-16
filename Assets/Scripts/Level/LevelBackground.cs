using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour
    {

        [SerializeField] public float startPositionY;

        [SerializeField] public float endPositionY;

        [SerializeField] public float movingSpeedY;
        
        private void FixedUpdate()
        {
            var position = transform.position;
            if (position.y <= endPositionY)
            {
                transform.position = new Vector3(position.x, startPositionY, position.z);
            }

            transform.position -= new Vector3(position.x, movingSpeedY * Time.fixedDeltaTime, position.z);
        }

    }
}