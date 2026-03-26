using System;
using Sound;
using Spawn;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

namespace Units
{
    public enum InteractableType
    {
        Letter, HostileAnimal, Player, Missile
    }

    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class InteractableUnit : MonoBehaviour
    {
        [SerializeField] private UnitType _unitType;

        [Header("Sound")]
        [SerializeField] protected SoundUnit _characterSound;

        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected ObjectPool _objectPool;

        public abstract InteractableType InteractableType { get; }
        public UnitType UnitType => _unitType;

        public event Action OnDeath;
        public event Action OnAttack;
        public event Action OnDance;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }

        [Inject]
        public virtual void Construct(ObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        public virtual void InvokeCharacterSound()
        {
            if (_characterSound == null)
                return;

            _characterSound.PlayOneShot();
        }

        protected abstract void HandleCollision(InteractableUnit other);
        protected virtual void InvokeDeath() => OnDeath?.Invoke();
        protected virtual void InvokeAttack() => OnAttack?.Invoke();
        protected virtual void InvokeDance() => OnDance?.Invoke();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableUnit interactable))
                HandleCollision(interactable);
        }
    }
}