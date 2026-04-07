using UnityEngine;

namespace Sound
{
    public class SoundController
    {
        private readonly SoundEffectChannel _channel;
        private readonly AudioSource playOneShotSource;

        public SoundController(AudioSourcePool pool, SoundConfig config)
        {
            _channel = config.Channel;
            playOneShotSource = pool.CreateSource();

            _channel.OnSoundEffectRaised += OnSoundEffectRaised;
        }

        private void OnSoundEffectRaised(SoundUnit soundUnit)
        {
            if (soundUnit == null)
                return;

            playOneShotSource.spatialBlend = soundUnit.SpatialBlend;
            playOneShotSource.pitch = soundUnit.Pitch;
            playOneShotSource.PlayOneShot(soundUnit.Clip, soundUnit.Volume);
        }

        public void Dispose() => _channel.OnSoundEffectRaised -= OnSoundEffectRaised;
    }
}
