using System;

namespace InputControl
{
    public interface IInput : IService
    {
        public event Action OnMoveUp;
        public event Action OnMoveDown;
        public event Action OnGameOver;

        public void Enable();
        public void Disable();
    }
}
