using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "Sound Unit", menuName = "Scriptable Objects/Sound/Sound Unit")]
    public class SoundUnit : ScriptableObject
    {
        [SerializeField] private AudioClip _audio;
        [SerializeField, Range(0, 1f)] private float _volume = 1f;
        [SerializeField, Range(0.1f, 3f)] private float _pitch = 1f;
        [SerializeField, Range(0, 1f)] private float _spatialBlend = 1f;
        [SerializeField] private SoundEffectChannel _channel;

        public AudioClip Clip => _audio;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public float SpatialBlend => _spatialBlend;

        public void PlayOneShot() => _channel.RaiseSoundEffect(this);

        public void Play(AudioSource source, bool isLoop)
        {
            source.pitch = _pitch;
            source.spatialBlend = _spatialBlend;
            source.clip = _audio;
            source.volume = _volume;
            source.loop = isLoop;
            source.Play();
        }

        public void Stop(AudioSource source)
        {
            if (source.isPlaying && source.clip == _audio)
                source.Stop();
        }
    }
}
