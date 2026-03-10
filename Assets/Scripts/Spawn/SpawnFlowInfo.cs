using UnityEngine;

namespace Spawn
{
    [System.Serializable]
    public struct SpawnableWeightBinder
    {
        [SerializeField] private SpawnableType _type;
        [SerializeField, Range(0, 1)] private float _weight;

        public readonly SpawnableType Type => _type;
        public readonly float Weight => _weight;
    }

    [System.Serializable]
    public struct SpawnFlowInfo
    {
        [SerializeField] private SpawnableWeightBinder[] _binders;
        [SerializeField] private float _spawnDistance;

        public readonly SpawnableWeightBinder[] Binders => _binders;
        public readonly float SpawnDistance => _spawnDistance;
    }
}