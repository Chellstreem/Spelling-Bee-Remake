using UnityEngine;

namespace GameplayObjects
{
    public class Missile : InteractableObject
    {
        [SerializeField] private int _damage = 1;

        public override SpawnableType Type => SpawnableType.Missile;

        protected override void HandleCollision(InteractableObject other)
        {

        }
    }
}