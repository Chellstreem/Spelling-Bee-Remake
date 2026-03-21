using System;
using WordControl;
using Zenject;

namespace Units
{
    public class Letter : InteractableUnit
    {
        private WordController _wordController;

        public string Value { get; private set; } = "-";
        public override UnitType Type => UnitType.Letter;

        public event Action OnLetterUpdated;

        [Inject]
        public void Construct(WordController wordController) => _wordController = wordController;

        private void OnEnable() => UpdateValue();

        protected override void HandleCollision(InteractableUnit other)
        {
            if (other.Type != UnitType.Player)
                return;

            _wordController.OnValueChecked(Value);
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