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
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit _currectValueSound;
        [SerializeField] private SoundUnit _incorrectValueSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _correctValueEffect;
        [SerializeField] private ParticleEffectInfo _wrongValueEffect;
        [Inject] private WordController _wordController;

        public string Value { get; private set; } = "-";
        public event Action OnLetterUpdated;


        private void OnEnable() => UpdateValue();

        public override void HandleCollision(Unit other)
        {
            if (other.UnitType != UnitType.Player) return;

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