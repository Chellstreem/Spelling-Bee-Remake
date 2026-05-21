using System;
using UnityEngine;

namespace SoundModule
{
    [CreateAssetMenu(fileName = "Sound Effect Channel", menuName = "Game/Sound/Sound Effect Channel")]
    public class SoundEffectChannel : ScriptableObject
    {
        public event Action<SoundUnit> OnSoundEffectRaised;

        public void RaiseSoundEffect(SoundUnit soundUnit) => OnSoundEffectRaised?.Invoke(soundUnit);
    }
}