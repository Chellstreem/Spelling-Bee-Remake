using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "Sound Unit", menuName = "Scriptable Objects/Sound/Sound Unit")]
    public class SoundUnit : ScriptableObject
    {
        public enum PlayType { Single, Loop, OneShot }

        [SerializeField] private PlayType _playType;
        [SerializeField] private AudioClip _audio;
        [SerializeField, Range(0, 1f)] private float _volume = 1f;
        [SerializeField, Range(0.1f, 3f)] private float _pitch = 1f;
        [SerializeField] private SoundEventChannel _channel;

        public PlayType Type => _playType;
        public AudioClip Audio => _audio;
        public float Volume => _volume;
        public float Pitch => _pitch;

        public void InvokeSound() => _channel.RaiseEvent(this);

        public void Play(AudioSource source)
        {
            source.pitch = _pitch;

            switch (_playType)
            {
                case PlayType.OneShot:
                    source.PlayOneShot(_audio, _volume);
                    return;
            }

            source.clip = _audio;
            source.volume = _volume;
            source.loop = _playType == PlayType.Loop;
            source.Play();
        }
    }
}
