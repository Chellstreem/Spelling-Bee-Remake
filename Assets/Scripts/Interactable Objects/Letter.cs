using System;
using Zenject;

namespace InteractableObjects
{
    public class Letter : InteractableObject
    {
        private ILetterProvider _letterProvider;

        public string Value { get; private set; }
        public override InteractableObjectType Type => InteractableObjectType.Letter;

        public event Action<string> OnLetterSet;

        [Inject]
        public void Construct(ILetterProvider letterProvider)
        {
            _letterProvider = letterProvider;
        }

        private void OnEnable() => InitializeValue();

        private void InitializeValue()
        {
            string letter = GetRandomLetter();
            OnLetterSet?.Invoke(letter);
            Value = letter.ToLower();
        }

        protected virtual string GetRandomLetter() => _letterProvider.GetRandomLetter();
    }
}