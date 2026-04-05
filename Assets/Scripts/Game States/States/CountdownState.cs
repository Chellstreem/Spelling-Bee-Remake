using System.Collections;
using UnityEngine;
using Sound;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Countdown State", menuName = "Game States/Countdown State")]
    public class CountdownState : GameStateDefinition
    {
        [SerializeField] private int _count = 3;
        [SerializeField] private int _startFontSize = 320;
        [SerializeField] private int _fontSizeDecrement = 45;
        [SerializeField] private int _finalFontSize = 350;
        [SerializeField] private string _startText = "GO!";

        [Header("Sound")]
        [SerializeField] private SoundUnit _tickSound;
        [SerializeField] private SoundUnit _startSound;

        private readonly WaitForSeconds wait = new(1f);

        public delegate void CountDown(string text, int fontSize);
        public event CountDown OnCountDownUpdate;

        public override void Enter(GameState state)
        {
            base.Enter(state);
            state.Context.Get<CoroutineRunner>().Run(CountDownCoroutine(_count, _startFontSize, state.Context.Get<GameStateController>()));
        }

        public override void Exit(GameState state) { }

        private IEnumerator CountDownCoroutine(int count, int fontSize, GameStateController stateController)
        {
            while (count > 0)
            {
                OnCountDownUpdate?.Invoke(count.ToString(), fontSize);
                _tickSound.PlayOneShot();

                yield return wait;

                count--;
                fontSize = Mathf.Max(1, fontSize - _fontSizeDecrement);
            }

            fontSize = _finalFontSize;
            OnCountDownUpdate?.Invoke(_startText, fontSize);
            _startSound.PlayOneShot();
            yield return wait;

            stateController.SetState(GameStateType.Interactive);
        }
    }
}