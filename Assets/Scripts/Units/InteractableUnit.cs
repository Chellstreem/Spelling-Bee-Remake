using System;
using Spawn;
using UnityEngine;
using Zenject;

namespace Units
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class InteractableUnit : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        protected ObjectPool _pool;

        public abstract UnitType Type { get; }

        public event Action OnDeath;
        public event Action OnAttack;
        public event Action OnDance;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        [Inject]
        public virtual void Construct(ObjectPool pool) => _pool = pool;
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