using UnityEngine;

namespace Units
{
    public class UnitStatus
    {
        public InteractableUnit Unit { get; }
        public ConstantUnitStatusDefinition Definition { get; }
        public ParticleSystem StatusEffect { get; set; }

        public UnitStatus(InteractableUnit unit, ConstantUnitStatusDefinition definition)
        {
            Unit = unit;
            Definition = definition;
        }

        public void HandleCollision(InteractableUnit other) => Definition.HandleCollision(this, other);
        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
    }
}