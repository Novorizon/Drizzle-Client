using MVC;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Input
{
    public static class GameInput
    {
        [DomainReload]
        private static GameControls _ctrl;

        public static GameControls Controller
        {
            get
            {
                if (_ctrl == null)
                    _ctrl = new GameControls();

                return _ctrl;
            }
        }

        public static void Enable()
        {
            Controller.Enable();
        }

        public static void Disable()
        {
            Controller.Disable();
        }

        public static bool EnhancedTouchEnabled => EnhancedTouchSupport.enabled;

        public static void EnableEnhancedTouch()
        {
            EnhancedTouchSupport.Enable();
        }

        public static void DisableEnhancedTouch()
        {
            EnhancedTouchSupport.Disable();
        }
    }
}
