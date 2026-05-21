using System;

namespace InputModule
{
    public interface IInput
    {
        public event Action OnKeyUp;
        public event Action OnKeyDown;
        public event Action OnKeyEscape;

        public void Enable();
        public void Disable();
    }
}
