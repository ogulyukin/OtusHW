using UnityEngine;

namespace UniversalComponents
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField]
        private bool isPlayer;
        public bool IsPlayer => isPlayer;
    }
}