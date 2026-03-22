using System;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "Sound Effect Channel", menuName = "Scriptable Objects/Sound/Sound Effect Channel")]
    public class SoundEffectChannel : ScriptableObject
    {
        public event Action<SoundUnit> OnSoundEffectRaised;

        public void RaiseSoundEffect(SoundUnit soundUnit) => OnSoundEffectRaised?.Invoke(soundUnit);
    }
}