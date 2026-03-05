using System;
using UnityEngine;

namespace InputSystem
{
    public class TouchInput : IInput, IInputEnabler
    {
        private readonly Controls controls;

        public event Action ClickUp;
        public event Action ClickDown;
        public event Action ClickDeath;

        public TouchInput()
        {
            controls = new Controls();

            controls.Gameplay.ChangePosition.performed += ctx => OnSwipe(ctx.ReadValue<Vector2>());
            controls.Gameplay.Die.performed += _ => ClickDeath?.Invoke();
        }

        public void Enable() => controls.Enable();
        public void Disable() => controls.Disable();

        public void OnSwipe(Vector2 delta)
        {
            if (delta.y > 0)
                ClickUp?.Invoke();
            else if (delta.y < 0)
                ClickDown?.Invoke();
        }
    }
}

