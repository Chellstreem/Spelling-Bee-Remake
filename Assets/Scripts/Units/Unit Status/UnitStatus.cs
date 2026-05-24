using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Units
{
    public class UnitStatus
    {
        public ComplexUnit Unit { get; }
        public UnitStatusDefinition Definition { get; }
        public ParticleSystem StatusEffect { get; set; }
        public CancellationTokenSource StatusCTS { get; set; }
        public float Duration { get; set; }

        public UnitStatus(ComplexUnit unit, UnitStatusDefinition definition)
        {
            Unit = unit;
            Definition = definition;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
    }
}