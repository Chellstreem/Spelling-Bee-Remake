using System;
using UnityEngine.InputSystem;

namespace InputModule
{
    public class DesktopInput : IInput
    {
        private readonly Controls controls = new();

        public event Action OnKeyUp;
        public event Action OnKeyDown;
        public event Action OnKeyEscape;

        public DesktopInput()
        {
            controls.Gameplay.GoUp.performed += MoveUp;
            controls.Gameplay.GoDown.performed += MoveDown;
            controls.Gameplay.Die.performed += CallGameOver;
        }

        void IInput.Enable() => controls.Enable();
        void IInput.Disable() => controls.Disable();

        private void MoveUp(InputAction.CallbackContext context) => OnKeyUp?.Invoke();
        private void MoveDown(InputAction.CallbackContext context) => OnKeyDown?.Invoke();
        private void CallGameOver(InputAction.CallbackContext context) => OnKeyEscape?.Invoke();

        public void Dispose()
        {
            controls.Gameplay.GoUp.performed -= MoveUp;
            controls.Gameplay.GoDown.performed -= MoveDown;
            controls.Gameplay.Die.performed -= CallGameOver;
        }
    }
}
