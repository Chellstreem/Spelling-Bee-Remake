using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        private Transform playerTransform;

        [Inject]
        public void Construct([Inject(Id = InstantiatedObjectType.Player)] Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }

        protected abstract void OnPlayerCollision(Transform playerTransform);        

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.transform == playerTransform)            
                OnPlayerCollision(playerTransform);                     
        }
    }
}
