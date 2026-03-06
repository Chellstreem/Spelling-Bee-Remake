using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Countdown State", menuName = "Game States/Countdown State")]
    public class CountdownState : GameState
    {
        [SerializeField] private int _count = 3;
        [SerializeField] private int _fontSize = 320;
        [SerializeField] private int _fontSizeDecrement = 45; // Насколько уменьшается размер шрифта при каждом тике    
        [SerializeField] private int _finalFontSize = 350; // Размер шрифта для надписи Go!

        private GameStateController _stateController;
        private EventManager _eventManager;
        private CoroutineRunner _coroutineRunner;

        public override GameStateType StateType => GameStateType.Countdown;

        public override void Initialize(GameStateController stateController, EventManager eventManager, CoroutineRunner runner)
        {
            _stateController = stateController;
            _eventManager = eventManager;
            _coroutineRunner = runner;
        }

        public override void Enter()
        {
            _eventManager.Publish(new OnCountdownStateEnter());
            _coroutineRunner.StartCor(CountDown(_count, _fontSize, _finalFontSize));
        }

        private IEnumerator CountDown(int count, int fontSize, int finalFontSize)
        {
            while (count >= 0)
            {
                _eventManager.Publish(new OnCountdownTick(count, fontSize, finalFontSize));
                yield return new WaitForSeconds(1f);
                count--;
                fontSize -= _fontSizeDecrement;
            }

            _stateController.SetState(GameStateType.Interactive);
        }

        public override void Exit()
        {
            _eventManager.Publish(new OnCountdownStateExit());
        }
    }
}