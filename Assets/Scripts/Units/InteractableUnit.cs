using System;
using Sound;
using Spawn;
using UnityEngine;
using Zenject;

namespace Units
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class InteractableUnit : MonoBehaviour
    {
        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected ObjectPool _objectPool;
        protected AudioSourcePool _audioPool;

        public abstract UnitType Type { get; }

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
        public virtual void Construct(ObjectPool objectPool, AudioSourcePool audioPool)
        {
            _objectPool = objectPool;
            _audioPool = audioPool;
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