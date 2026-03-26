using UnityEngine;
using VFX;

namespace Units
{
    public abstract class UnitStatusDefinition : ScriptableObject
    {
        [SerializeField] private UnitStatusType _type;
        [SerializeField] private bool _canTakeDamage = true;
        [SerializeField] private bool _canDealDamage = true;
        [SerializeField] private bool _canMove = true;
        [SerializeField] protected ParticleEffectInfo _statusParticle;

        public UnitStatusType Type => _type;
        public bool CanTakeDamage => _canTakeDamage;
        public bool CanDealDamage => _canDealDamage;
        public bool CanMove => _canMove;

        public UnitStatus CreateStatus() => new(this);
        public abstract void HandleCollision(InteractableUnit unit, InteractableUnit other);
        public abstract void Enter(InteractableUnit unit);
        public abstract void Exit(InteractableUnit unit);
    }
}