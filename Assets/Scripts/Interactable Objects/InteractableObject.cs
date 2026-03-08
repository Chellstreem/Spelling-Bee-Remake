using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        protected ISpawnableObjectReturner _objectReturner;
        public abstract InteractableObjectType Type { get; }

        public delegate void OnCollision(InteractableObject sender, InteractableObject other);
        public event OnCollision OnInteractableCollision;

        [Inject]
        public virtual void Construct(ISpawnableObjectReturner objectReturner) => _objectReturner = objectReturner;

        protected virtual void HandleCollision(InteractableObject other)
        {
            OnInteractableCollision?.Invoke(this, other);
            _objectReturner.ReturnObject(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
                HandleCollision(interactable);
        }
    }
}