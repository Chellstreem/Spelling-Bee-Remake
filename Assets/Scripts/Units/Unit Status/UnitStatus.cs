using UnityEngine;

namespace Units
{
    public class UnitStatus
    {
        public UnitStatusDefinition Definition { get; }
        public ParticleSystem StatusEffect { get; set; }

        public UnitStatus(UnitStatusDefinition definition)
        {
            Definition = definition;
        }
    }
}