using System;
using UnityEngine;

namespace InputSystem
{
    public class DesktopInput : IInput, IInputEnabler
    {
        private readonly Controls controls;

        public event Action ClickUp;
        public event Action ClickDown;
        public event Action ClickDeath;

        public DesktopInput()
        {
            controls = new Controls();

            controls.Gameplay.GoUp.performed += _ => ClickUp?.Invoke();
            controls.Gameplay.GoDown.performed += _ => ClickDown?.Invoke();
            controls.Gameplay.Die.performed += _ => ClickDeath?.Invoke();            
        }

        public void Enable() => controls.Enable();
        public void Disable() => controls.Disable();         
    }
}
