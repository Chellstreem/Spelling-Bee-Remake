using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Interactive State", menuName = "Game States/Interactive State")]
    public class InteractiveState : SpawnState, IEventSubscriber<OnWordCompleted>
    {
        private GameStateController _stateController;
        private CoroutineRunner _coroutineRunner;

        public override GameStateType StateType => GameStateType.Interactive;

        public override void Construct(GameStateController stateController, CoroutineRunner runner)
        {
            _stateController = stateController;
            _coroutineRunner = runner;
        }

        public override void Enter()
        {
            Debug.Log("Entering Interactive State...");
            SubscribeToEvents();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Interactive State...");
            UnsubscribeFromEvents();
        }

        public void OnEvent(OnWordCompleted eventData)
        {
            if (eventData.GameplayAction == GameplayActionType.Missiles)
            {
                _stateController.SetState(GameStateType.Missile);
            }
            else
            {
                _stateController.SetState(GameStateType.Safe);
            }
        }

        private void SubscribeToEvents() { }// _eventManager.Subscribe<OnWordCompleted>(this);
        private void UnsubscribeFromEvents() { }// _eventManager.Unsubscribe<OnWordCompleted>(this);
    }
}
