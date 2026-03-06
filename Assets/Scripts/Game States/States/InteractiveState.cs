using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Interactive State", menuName = "Game States/Interactive State")]
    public class InteractiveState : GameState, IEventSubscriber<OnWordCompleted>
    {
        private EventManager _eventManager;
        private GameStateController _stateController;

        public override GameStateType StateType => GameStateType.Interactive;

        public override void Initialize(GameStateController stateController, EventManager eventManager, CoroutineRunner runner)
        {
            _eventManager = eventManager;
            _stateController = stateController;
        }

        public override void Enter()
        {
            Debug.Log("Entering Interactive State...");
            _eventManager.Publish(new OnInteractiveSubstateEnter());
            SubscribeToEvents();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Interactive State...");
            _eventManager.Publish(new OnInteractiveSubstateExit());
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

        private void SubscribeToEvents() => _eventManager.Subscribe<OnWordCompleted>(this);
        private void UnsubscribeFromEvents() => _eventManager.Unsubscribe<OnWordCompleted>(this);
    }
}
