using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField]
        private bool isPlayer;
        public bool IsPlayer => isPlayer;
    }
}