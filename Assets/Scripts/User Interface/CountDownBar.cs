using GameStates;
using TMPro;
using UnityEngine;
using Zenject;

namespace UserInterface
{
    public class CountDownBar : UIBar
    {
        [SerializeField] private TextMeshProUGUI _text;
        private CountdownState _state;

        [Inject]
        public void Construct(GameStateController stateController)
        {
            _state = stateController.GetGameState(GameStateType.Countdown).Definition as CountdownState;
        }

        private void Start()
        {
            if (_state == null)
                return;

            _state.OnCountDownUpdate += OnCountDownUpdate;
        }

        private void OnCountDownUpdate(string text, int fontSize)
        {
            _text.fontSize = fontSize;
            _text.text = text;
        }

        private void OnDisable() => _state.OnCountDownUpdate -= OnCountDownUpdate;
    }
}