using System;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public class OrbLetter : InteractableObject, ILetter
    {
        private EventManager eventManager;        
        private ISpawnableObjectReturner returner;
        private ILetterProvider letterProvider;

        public string Value { get; private set; }
        public event Action<string> OnLetterSet;

        [Inject]
        public void Construct(EventManager eventManager, ISpawnableObjectReturner returner, ILetterProvider letterProvider)
        {
            this.eventManager = eventManager;  
            this.letterProvider = letterProvider;
            this.returner = returner;              
        }

        private void OnEnable() => InitializeValue();

        private void InitializeValue()
        {
            string letter = SetRandomLetter();
            OnLetterSet?.Invoke(letter);
            Value = letter.ToLower();
        }
        
        protected virtual string SetRandomLetter() => letterProvider.GetRandomLetter();           

        protected override void OnPlayerCollision(Transform playerTransform)
        {
            eventManager.Publish(new OnLetterCollision(Value, transform.position));
            ReturnToOriginalState();          
        }

        protected virtual void ReturnToOriginalState()
        {
            returner.ReturnObject(gameObject);
        }
    }
}
