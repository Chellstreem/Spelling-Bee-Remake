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


    }
}
