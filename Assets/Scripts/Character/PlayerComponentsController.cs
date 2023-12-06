using UniversalComponents;

namespace Character
{
    public sealed class PlayerComponentsController : CustomComponentsController
    {
        protected override void Awake()
        {
            base.Awake();
            FireConfig = new PlayerFireConfig();
        }
    }
}
