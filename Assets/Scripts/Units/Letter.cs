using System;
using WordControl;
using Zenject;
using UnityEngine;
using Sound;

namespace Units
{
    public class Letter : InteractableUnit
    {
        [SerializeField] private SoundUnit _currectValueSound;
        [SerializeField] private SoundUnit _incorrectValueSound;
        private WordController _wordController;

        public string Value { get; private set; } = "-";
        public override InteractableType Type => InteractableType.Letter;

        public event Action OnLetterUpdated;

        [Inject]
        public void Construct(WordController wordController) => _wordController = wordController;

        private void OnEnable() => UpdateValue();

        protected override void HandleCollision(InteractableUnit other)
        {
            if (other.Type != InteractableType.Player)
                return;

            if (_wordController.IsCorrect(Value))
                _currectValueSound.PlayOneShot();
            else
                _incorrectValueSound.PlayOneShot();

            _objectPool.ReturnObject(gameObject);
        }

        private void UpdateValue()
        {
            if (_wordController == null)
                return;

            Value = _wordController.GetRandomSymbol().ToLower();
            OnLetterUpdated?.Invoke();
        }
    }
}