namespace MovableObjects
{
    public class NonInteractableObject : MovableObject
    {
        private void OnEnable() => _stateController.OnStateChanged += OnStateChanged;

        private void OnStateChanged()
        {
            if (!_stateController.CurrentState.AllowMoving)
            {
                StopMoving();
                return;
            }
        }

        private void OnDisable()
        {
            StopMoving();
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
