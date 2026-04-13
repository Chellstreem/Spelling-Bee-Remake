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
        [Tooltip("Damage value applied by this unit when it attacks")]
        [SerializeField] protected int _damage = 0;

        [Header("Statuses")]
        [Tooltip("Default status assigned to the unit on spawn")]
        [SerializeField] private UnitStatusType _defaultStatus = UnitStatusType.Normal;
        [Tooltip("Definitions of status types available to this unit")]
        [SerializeField] private UnitStatusDefinition[] _statuses;

        [Header("Sound")]
        [Tooltip("Sound played when the unit performs an attack")]
        [SerializeField] private SoundUnit _attackSound;
        [Tooltip("Sound played when the unit dies")]
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