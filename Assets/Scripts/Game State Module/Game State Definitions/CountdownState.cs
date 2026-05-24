using Cysharp.Threading.Tasks;
using UnityEngine;
using SoundModule;

namespace GameStateModule
{
    [CreateAssetMenu(fileName = "Countdown State", menuName = "Game/Game States/Countdown State")]
    public class CountdownState : GameStateDefinition
    {
        [Tooltip("Number of steps in the countdown")]
        [SerializeField] private int _count = 3;

        [Tooltip("Font size used for the first countdown number")]
        [SerializeField] private int _startFontSize = 320;

        [Tooltip("Amount to decrease font size on each countdown step")]
        [SerializeField] private int _fontSizeDecrement = 45;

        [Tooltip("Font size used for the final message")]
        [SerializeField] private int _finalFontSize = 350;

        [Tooltip("Text displayed on countdown completion (e.g. GO!)")]
        [SerializeField] private string _startText = "GO!";

        [Header("Sound")]
        [Tooltip("Sound played each tick of the countdown")]
        [SerializeField] private SoundUnit _tickSound;

        [Tooltip("Sound played on countdown start/completion")]
        [SerializeField] private SoundUnit _startSound;

        public delegate void CountDown(string text, int fontSize);
        public event CountDown OnCountDownUpdate;

        public override void Enter(GameState state) => RunCountdownAsync(state.StateController).Forget();
        public override void Exit(GameState state) { }

        private async UniTaskVoid RunCountdownAsync(GameStateController stateController)
        {
            int count = _count;
            int fontSize = _startFontSize;

            while (count > 0)
            {
                OnCountDownUpdate?.Invoke(count.ToString(), fontSize);
                _tickSound.PlayOneShot();

                await UniTask.Delay(1000);

                count--;
                fontSize = Mathf.Max(1, fontSize - _fontSizeDecrement);
            }

            OnCountDownUpdate?.Invoke(_startText, _finalFontSize);
            _startSound.PlayOneShot();

            await UniTask.Delay(1000);

            stateController.SetState(GameStateType.Interactive);
        }
    }
}