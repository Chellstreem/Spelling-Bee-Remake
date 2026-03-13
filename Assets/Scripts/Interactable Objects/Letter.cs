using System;
using WordControl;
using Zenject;

namespace InteractableObjects
{
    public class Letter : InteractableObject
    {
        private WordController _wordController;

        public string Value { get; private set; } = "-";
        public override SpawnableType Type => SpawnableType.Letter;

        public event Action OnLetterUpdated;

        [Inject]
        public void Construct(WordController wordController) => _wordController = wordController;
        private void OnEnable() => UpdateValue();

        protected override void HandleCollision(InteractableObject other)
        {
            if (other.Type != SpawnableType.Player)
                return;

            _wordController.CheckLetter(Value);
            _pool.ReturnObject(gameObject);
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