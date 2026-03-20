using UnityEngine;
using System;

namespace Sound
{
    [CreateAssetMenu(fileName = "Sound Event Channel", menuName = "Scriptable Objects/Sound/Sound Event Channel")]
    public class SoundEventChannel : ScriptableObject
    {
        public Action<SoundUnit> OnSoundEventRaised;

        public void RaiseEvent(SoundUnit unit) => OnSoundEventRaised?.Invoke(unit);
    }
}