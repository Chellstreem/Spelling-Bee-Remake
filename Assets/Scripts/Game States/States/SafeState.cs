using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Safe State", menuName = "Game States/Safe State")]
    public class SafeState : GameState
    {
        [SerializeField] private float _duration = 3f;
        private GameStateController _stateController;
        private CoroutineRunner _coroutineRunner;
        private Coroutine _coroutine;

        public override GameStateType StateType => GameStateType.Safe;

        public override void Initialize(GameStateController stateController, EventManager eventManager, CoroutineRunner runner)
        {
            _stateController = stateController;
            _coroutineRunner = runner;
        }

        public override void Enter()
        {
            Debug.Log("Entering Safe State...");
            _coroutine = _coroutineRunner.StartCor(SafetyCoroutine(_duration));
        }

        public override void Exit()
        {
            Debug.Log("Exiting Safe State...");
            StopSafetyCoroutine();
        }

        private IEnumerator SafetyCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            _stateController.SetState(GameStateType.Interactive);
        }

        private void StopSafetyCoroutine()
        {
            if (_coroutine != null)
            {
                _coroutineRunner.StopCor(_coroutine);
                _coroutine = null;
            }
        }
    }
}
