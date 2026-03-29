using UnityEngine;

namespace Units
{
    public class UnitStatus
    {
        public ComplexUnit Unit { get; }
        public UnitStatusDefinition Definition { get; }
        public CoroutineRunner CoroutineRunner { get; }

        public ParticleSystem StatusEffect { get; set; }
        public Coroutine StatusCoroutine { get; set; }
        public float Duration { get; set; }

        public UnitStatus(ComplexUnit unit, UnitStatusDefinition definition, CoroutineRunner coroutineRunner)
        {
            Unit = unit;
            Definition = definition;
            CoroutineRunner = coroutineRunner;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
    }
}