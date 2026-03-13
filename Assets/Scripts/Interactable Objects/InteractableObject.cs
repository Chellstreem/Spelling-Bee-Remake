using Spawn;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        protected ObjectPool _pool;
        public abstract SpawnableType Type { get; }

        public delegate void OnCollision(InteractableObject sender);
        public event OnCollision OnInteractableCollision;

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