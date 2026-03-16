using System.ComponentModel;
using System.Collections;
using UnityEngine;

namespace GameStates
{
    [CreateAssetMenu(fileName = "Countdown State", menuName = "Game States/Countdown State")]
    public class CountdownState : GameStateDefinition
    {
        [SerializeField] private int _count = 3;
        [SerializeField] private int _startFontSize = 320;
        [SerializeField] private int _fontSizeDecrement = 45;
        [SerializeField] private int _finalFontSize = 350;
        [SerializeField] private string _text = "Go!";

        private readonly WaitForSeconds wait = new(1f);
        public override GameStateType StateType => GameStateType.Countdown;

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
                Debug.Log(count);
                yield return wait;
                count--;
                fontSize = Mathf.Max(1, fontSize - _fontSizeDecrement);
            }

            Debug.Log(_text);
            fontSize = _finalFontSize;
            yield return wait;
            stateController.SetState(GameStateType.Interactive);
        }
    }
}