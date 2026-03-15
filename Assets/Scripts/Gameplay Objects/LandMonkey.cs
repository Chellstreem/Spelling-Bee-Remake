using UnityEngine;

namespace GameplayObjects
{
    public class LandMonkey : InteractableObject
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _damage = 1;

        public override SpawnableType Type => SpawnableType.LandMonkey;

        protected override void HandleCollision(InteractableObject other)
        {
            if (other.Type == SpawnableType.Letter)
                _pool.ReturnObject(other.gameObject);
        }
    }
}
