using UnityEngine;
using Units;

namespace Spawn
{
    [System.Serializable]
    public struct SpawnBinder
    {
        [Tooltip("Type of unit to spawn")]
        [SerializeField] private UnitType _type;
        [Tooltip("Relative spawn weight used when selecting among binders")]
        [SerializeField, Range(0.01f, 1)] private float _weight;
        [Tooltip("Probability multiplier applied to this binder when spawning")]
        [SerializeField, Range(0.01f, 1)] private float _spawnProbabilty;

        public readonly UnitType Type => _type;
        public readonly float Weight => _weight;
        public readonly float SpawnProbabilty => _spawnProbabilty;
    }

    [System.Serializable]
    public struct SpawnFlowInfo
    {
        [Tooltip("Delay in seconds from the start of this flow until its first spawn")]
        [SerializeField] private float _delay;
        [Tooltip("Binders that define what units and weights are spawned in this flow")]
        [SerializeField] private SpawnBinder[] _binders;
        [Tooltip("Distance in world units at which spawned units appear relative to spawn origin")]
        [SerializeField] private float _spawnDistance;

        public readonly float SpawnDelay => _delay;
        public readonly SpawnBinder[] Binders => _binders;
        public readonly float SpawnDistance => _spawnDistance;
    }
}