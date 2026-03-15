using Spawn;
using UnityEngine;
using Zenject;

namespace GameplayObjects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class InteractableObject : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        protected ObjectPool _pool;
        public abstract SpawnableType Type { get; }

        public delegate void OnCollision(InteractableObject sender);
        public event OnCollision OnInteractableCollision;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        [Inject]
        public virtual void Construct(ObjectPool pool) => _pool = pool;

        protected abstract void HandleCollision(InteractableObject other);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
                HandleCollision(interactable);
        }
    }
}