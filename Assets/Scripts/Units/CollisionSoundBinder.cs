using Sound;
using UnityEngine;

namespace Units
{
    [System.Serializable]
    public struct CollisionSoundBinder
    {
        [SerializeField] private UnitType _unit;
        [SerializeField] private SoundUnit _soundUnit;

        public readonly UnitType UnitType => _unit;
        public readonly SoundUnit SoundUnit => _soundUnit;
    }
}