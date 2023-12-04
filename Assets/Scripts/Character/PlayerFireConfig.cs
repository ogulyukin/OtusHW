using UnityEngine;
using UniversalComponents;

namespace Character
{
    public class PlayerFireConfig : MonoBehaviour, IFireControl
    {
        public Vector2 GetFireDirection()
        {
            return Vector2.up;
        }
    }
}
