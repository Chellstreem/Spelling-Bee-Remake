using UnityEngine;

namespace Sound
{
    public enum PlayType { Single, Loop, OneShot }

    [CreateAssetMenu(fileName = "Sound Unit", menuName = "Scriptable Objects/Sound/Sound Unit")]
    public class SoundUnit : ScriptableObject
    {
        [SerializeField] private PlayType _playType;
        [SerializeField] private AudioClip _audio;
        [SerializeField, Range(0, 1f)] private float _volume = 1f;
        [SerializeField, Range(0.1f, 3f)] private float _pitch = 1f;
        [SerializeField] private SoundEventChannel _channel;

        public PlayType PlayType => _playType;
        public AudioClip Audio => _audio;
        public float Volume => _volume;
        public float Pitch => _pitch;

        public void InvokeSound() => _channel.RaiseEvent(this);
    }
}
