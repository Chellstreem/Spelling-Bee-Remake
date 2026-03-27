using UnityEngine;
using VFX;

namespace Units
{
    [CreateAssetMenu(fileName = "Constant Status", menuName = "Scriptable Objects/Unit Status/Constant Status")]
    public class ConstantUnitStatusDefinition : ScriptableObject
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

        public UnitStatus CreateStatus(InteractableUnit unit) => new(unit, this);

        public virtual void HandleCollision(UnitStatus status, InteractableUnit other)
        {
            status.Unit.HandleCollision(other);
        }

        public virtual void Enter(UnitStatus status)
        {
            if (_statusParticle.Type != ParticleType.None)
                status.StatusEffect = status.Unit.UseParticleEffect(_statusParticle);
        }

        public virtual void Exit(UnitStatus status)
        {
            if (status.StatusEffect != null)
                status.StatusEffect.Stop();
        }
    }
}