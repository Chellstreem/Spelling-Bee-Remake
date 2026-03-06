using System;
using GameStates;

namespace PlayerMobility
{
    public class PlayerMovementHandler : IDisposable
    {
        private readonly GameStateController _stateController;
        private readonly IPlayerMover playerMover;
        private readonly IInput input;
        private readonly IDeathInvoker deathInvoker;

        public PlayerMovementHandler(GameStateController stateController, IPlayerMover playerMover, IDeathInvoker deathInvoker,
            IInput input)
        {
            _stateController = stateController;
            this.playerMover = playerMover;
            this.input = input;
            this.deathInvoker = deathInvoker;

            SubscribeToEvents();
        }

        private void OnStateChanged()
        {
            if (_stateController.CurrentState.StateType == GameStateType.Victory)
                playerMover.GoDown();
        }

        public void Dispose() => UnsubscribeFromEvents();

        private void SubscribeToEvents()
        {
            input.ClickUp += playerMover.GoUp;
            input.ClickDown += playerMover.GoDown;
            input.ClickDeath += deathInvoker.InvokeDeath;
            _stateController.OnStateChanged += OnStateChanged;
        }

        private void UnsubscribeFromEvents()
        {
            input.ClickUp -= playerMover.GoUp;
            input.ClickDown -= playerMover.GoDown;
            input.ClickDeath -= deathInvoker.InvokeDeath;
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
