using UnityEngine;

namespace Level
{
    public class LevelBackgroundConfig : MonoBehaviour
    {
        [SerializeField] public float startPositionY;
        [SerializeField] public float endPositionY;
        [SerializeField] public float movingSpeedY;

        public float StartPositionY => startPositionY;

        public float EndPositionY => endPositionY;

        public float MovingSpeedY => movingSpeedY;
    }
}
