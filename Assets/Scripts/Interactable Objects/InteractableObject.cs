using Spawn;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        protected ObjectPool _pool;

        public abstract GameCharacterType Type { get; }
        public delegate void OnCollision(InteractableObject sender, InteractableObject other);
        public event OnCollision OnInteractableCollision;

        [Inject]
        public virtual void Construct(ObjectPool pool) => _pool = pool;

        protected virtual void HandleCollision(InteractableObject other)
        {
            OnInteractableCollision?.Invoke(this, other);
            _pool.ReturnObject(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
                HandleCollision(interactable);
        }
    }
}