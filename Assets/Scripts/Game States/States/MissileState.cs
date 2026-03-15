using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Missile State", menuName = "Game States/Missile State")]
    public class MissileState : SpawnStateDefinition
    {
        [SerializeField] private float _duration = 5f;

        public override GameStateType StateType => GameStateType.Missile;

        public override void Enter(GameState state)
        {
            Debug.Log("Entering Missile State...");
            StartMissileCoroutine();
        }

        public override void Exit(GameState state)
        {
            Debug.Log("Exiting Missile State...");
            StopMissileCoroutine();
        }

        private IEnumerator MissileCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            _stateController.SetState(GameStateType.Safe);
        }

        private void StartMissileCoroutine() => _coroutine = _coroutineRunner.StartCoroutine(MissileCoroutine(_duration));

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
