using System;
using UnityEngine.InputSystem;

namespace Input
{
    public class DesktopInput : IInput
    {
        private readonly Controls controls = new();

        public event Action OnMoveUp;
        public event Action OnMoveDown;
        public event Action OnGameOver;

        public DesktopInput()
        {
            controls.Gameplay.GoUp.performed += MoveUp;
            controls.Gameplay.GoDown.performed += MoveDown;
            controls.Gameplay.Die.performed += CallGameOver;
        }

        private void MoveUp(InputAction.CallbackContext context) => OnMoveUp?.Invoke();
        private void MoveDown(InputAction.CallbackContext context) => OnMoveDown?.Invoke();
        public void CallGameOver(InputAction.CallbackContext context) => OnGameOver?.Invoke();
        public void Enable() => controls.Enable();
        public void Disable() => controls.Disable();
    }
}
