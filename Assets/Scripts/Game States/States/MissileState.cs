using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Missile State", menuName = "Game States/Missile State")]
    public class MissileState : GameState
    {
        [SerializeField] private float duration = 5f;
        private GameStateController _stateController;
        private EventManager _eventManager;
        private CoroutineRunner _coroutineRunner;
        private Coroutine _coroutine;

        public override GameStateType StateType => GameStateType.Missile;

        public override void Initialize(GameStateController stateController, EventManager eventManager, CoroutineRunner runner)
        {
            _stateController = stateController;
            _eventManager = eventManager;
            _coroutineRunner = runner;
        }

        public override void Enter()
        {
            Debug.Log("Entering Missile State...");
            _eventManager.Publish(new OnMissileStateEnter());
            StartMissileCoroutine();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Missile State...");
            _eventManager.Publish(new OnMissileStateExit());
            StopMissileCoroutine();
        }

        private IEnumerator MissileCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            _stateController.SetState(GameStateType.Safe);
        }

        private void StartMissileCoroutine() => _coroutine = _coroutineRunner.StartCor(MissileCoroutine(duration));

        private void StopMissileCoroutine()
        {
            if (_coroutine != null)
            {
                _coroutineRunner.StopCor(_coroutine);
                _coroutine = null;
            }
        }
    }
}
