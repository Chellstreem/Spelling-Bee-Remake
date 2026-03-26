using System;
using WordControl;
using Zenject;
using UnityEngine;
using Sound;
using VFX;

namespace Units
{
    public class Letter : InteractableUnit
    {
        [SerializeField] private SoundUnit _currectValueSound;
        [SerializeField] private SoundUnit _incorrectValueSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _correctValueEffect;
        [SerializeField] private ParticleEffectInfo _wrongValueEffect;

        private WordController _wordController;

        public string Value { get; private set; } = "-";
        public override InteractableType InteractableType => InteractableType.Letter;

        public event Action OnLetterUpdated;

        [Inject]
        public void Construct(WordController wordController) => _wordController = wordController;

        private void OnEnable() => UpdateValue();

        protected override void HandleCollision(InteractableUnit other)
        {
            switch (other.InteractableType)
            {
                case InteractableType.HostileAnimal:
                    _objectPool.ReturnObject(gameObject);
                    break;

                case InteractableType.Player:

                    Vector3 position = transform.position;

                    if (_wordController.IsCorrect(Value))
                    {
                        _currectValueSound.PlayOneShot();
                        PlayEffect(_correctValueEffect);
                    }
                    else
                    {
                        _incorrectValueSound.PlayOneShot();
                        PlayEffect(_wrongValueEffect);
                    }

                    _objectPool.ReturnObject(gameObject);
                    break;
            }
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