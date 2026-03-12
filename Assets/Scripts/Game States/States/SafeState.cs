using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Safe State", menuName = "Game States/Safe State")]
    public class SafeState : GameStateDefinition
    {
        [SerializeField] private float _duration = 3f;
        private GameStateController _stateController;
        private CoroutineRunner _coroutineRunner;
        private Coroutine _coroutine;

        public override GameStateType StateType => GameStateType.Safe;

        public override void Enter(GameState state)
        {
            Debug.Log("Entering Safe State...");
            _coroutine = _coroutineRunner.StartCoroutine(SafetyCoroutine(_duration));
        }

        public override void Exit(GameState state)
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
                _coroutineRunner.Stop(_coroutine);
                _coroutine = null;
            }
        }
    }
}
