using GameStates;

namespace InputControl
{
    public class InputController
    {
        private readonly IInput input;
        private readonly GameStateController stateController;

        public InputController(IInput input, GameStateController gameStateController)
        {
            this.input = input;
            stateController = gameStateController;

            stateController.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (stateController.CurrentState.Definition.EnableInput)
                input.Enable();
            else
                input.Disable();
        }

        public void Dispose() => stateController.OnStateChanged -= OnStateChanged;
    }
}