using System;
using Sound;
using Spawn;
using UnityEngine;
using VFX;
using Zenject;

namespace Units
{
    public enum ComplexUnitType
    {
        HostileAnimal, Player
    }

    public abstract class ComplexUnit : Unit
    {
        [SerializeField] protected int _damage = 0;

        [Header("Statuses")]
        [SerializeField] private UnitStatusType _defaultStatus = UnitStatusType.Normal;
        [SerializeField] private UnitStatusDefinition[] _statuses;

        [Header("Sound")]
        [SerializeField] private SoundUnit _attackSound;
        [SerializeField] private SoundUnit _deathSound;

        public abstract ComplexUnitType ComplexUnitType { get; }
        public UnitStatusType DefaultStatus => _defaultStatus;
        public UnitStatusDefinition[] Statuses => _statuses;
        public UnitStatusController StatusController { get; private set; }

        public event Action OnDeath;
        public event Action OnAttack;
        public event Action OnDance;

        protected override void Awake()
        {
            base.Awake();

            StatusController = new(this, _coroutineRunner);
            StatusController.SetStatus(_defaultStatus);
        }

        protected virtual void InvokeDeath()
        {
            IsInteractable = false;
            OnDeath?.Invoke();

            if (_deathSound != null)
                _deathSound.PlayOneShot();
        }

        protected virtual void InvokeAttack()
        {
            OnAttack?.Invoke();

            if (_attackSound != null)
                _attackSound.PlayOneShot();
        }

        protected virtual void InvokeDance() => OnDance?.Invoke();
    }
}