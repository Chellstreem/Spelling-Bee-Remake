using System;
using WordControl;
using Zenject;
using UnityEngine;
using Sound;
using VFX;

namespace Units
{
    public class Letter : Unit
    {
        [Tooltip("Damage applied to the player when this letter is incorrect (used for enemy letters)")]
        [SerializeField] private int _damage = 1;
        [Tooltip("Sound played when this letter is selected and is correct")]
        [SerializeField] private SoundUnit _currectValueSound;
        [Tooltip("Sound played when this letter is selected and is incorrect")]
        [SerializeField] private SoundUnit _incorrectValueSound;

        [Header("VFX")]
        [Tooltip("Particle effect played when the letter is correct")]
        [SerializeField] private ParticleEffectInfo _correctValueEffect;
        [Tooltip("Particle effect played when the letter is incorrect")]
        [SerializeField] private ParticleEffectInfo _wrongValueEffect;
        [Inject] private WordController _wordController;

        public string Value { get; private set; } = "-";
        public event Action OnLetterUpdated;


        private void OnEnable() => UpdateValue();

        public override void HandleCollision(Unit other)
        {
            if (_wordController.IsCorrect(Value))
            {
                _currectValueSound.PlayOneShot();
                ApplyParticleEffect(_correctValueEffect);
            }
            else
            {
                other.Damage(_damage);
                _incorrectValueSound.PlayOneShot();
                ApplyParticleEffect(_wrongValueEffect);
            }

            _objectPool.ReturnObject(gameObject);
        }

        public override void Damage(int damage) => _objectPool.ReturnObject(gameObject);

        private void UpdateValue()
        {
            if (_wordController == null)
                return;

            Value = _wordController.GetRandomSymbol().ToLower();
            OnLetterUpdated?.Invoke();
        }
    }
}