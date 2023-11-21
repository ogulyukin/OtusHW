using UnityEngine;

namespace UI
{
    public sealed class ExitButtonHandler : MonoBehaviour
    {
        public void ExitGameHandler()
        {
            Application.Quit();
        }
    }
}
