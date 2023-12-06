using UnityEngine;
using UniversalComponents;

namespace Character
{
    public sealed class PlayerFireConfig : IFireConfig
    {
        public Vector2 GetFireDirection()
        {
            return Vector2.up;
        }
    }
}
