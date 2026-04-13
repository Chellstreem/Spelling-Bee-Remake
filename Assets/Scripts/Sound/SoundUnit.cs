using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "Sound Unit", menuName = "Scriptable Objects/Sound/Sound Unit")]
    public class SoundUnit : ScriptableObject
    {
        [Tooltip("Audio clip that will be played by this sound unit")]
        [SerializeField] private AudioClip _audio;
        [Tooltip("Playback volume multiplier (0..1)")]
        [SerializeField, Range(0, 1f)] private float _volume = 1f;
        [Tooltip("Playback pitch multiplier (affects speed and pitch)")]
        [SerializeField, Range(0.1f, 3f)] private float _pitch = 1f;
        [Tooltip("Spatial blend between 2D and 3D audio (0 = 2D, 1 = 3D)")]
        [SerializeField, Range(0, 1f)] private float _spatialBlend = 1f;
        [Tooltip("Channel used to route this sound through the audio system")]
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
