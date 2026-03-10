using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Missile State", menuName = "Game States/Missile State")]
    public class MissileState : GameState
    {
        [SerializeField] private float duration = 5f;
        private GameStateController _stateController;
        private CoroutineRunner _coroutineRunner;
        private Coroutine _coroutine;

        public override GameStateType StateType => GameStateType.Missile;

        public override void Construct(GameStateController stateController, CoroutineRunner runner)
        {
            _stateController = stateController;
            _coroutineRunner = runner;
        }

        public override void Enter()
        {
            Debug.Log("Entering Missile State...");
            StartMissileCoroutine();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Missile State...");
            StopMissileCoroutine();
        }

        private IEnumerator MissileCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            _stateController.SetState(GameStateType.Safe);
        }

        private void StartMissileCoroutine() => _coroutine = _coroutineRunner.StartCoroutine(MissileCoroutine(duration));

        private void StopMissileCoroutine()
        {
            if (_coroutine != null)
            {
                _coroutineRunner.Stop(_coroutine);
                _coroutine = null;
            }
        }
    }
}
