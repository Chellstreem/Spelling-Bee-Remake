using System.ComponentModel;
using System.Collections;
using UnityEngine;
using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
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
        [SerializeField] private string _text = "GO!";

        [Header("Sound")]
        [SerializeField] private SoundUnit _tickSound;
        [SerializeField] private SoundUnit _startSound;

        private readonly WaitForSeconds wait = new(1f);
        public override GameStateType StateType => GameStateType.Countdown;

        public delegate void CountDown(string text, int fontSize);
        public event CountDown OnCountDownUpdate;

        public override void Enter(GameState state)
        {
            Debug.Log("Enter Count Down");
            state.Runner.Run(CountDownCoroutine(_count, _startFontSize, state.StateController));
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
            OnCountDownUpdate?.Invoke(_text, fontSize);
            _startSound.PlayOneShot();
            yield return wait;

            stateController.SetState(GameStateType.Interactive);
        }
    }
}