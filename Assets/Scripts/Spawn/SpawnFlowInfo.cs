using UnityEngine;

namespace Spawn
{
    [System.Serializable]
    public struct SpawnBinder
    {
        [SerializeField] private SpawnableType _type;
        [SerializeField, Range(0.01f, 1)] private float _weight;
        [SerializeField, Range(0.01f, 1)] private float _spawnProbabilty;

        public readonly SpawnableType Type => _type;
        public readonly float Weight => _weight;
        public readonly float SpawnProbabilty => _spawnProbabilty;
    }

    [System.Serializable]
    public struct SpawnFlowInfo
    {
        [SerializeField] private SpawnBinder[] _binders;
        [SerializeField] private float _spawnDistance;

        public readonly SpawnBinder[] Binders => _binders;
        public readonly float SpawnDistance => _spawnDistance;
    }
}