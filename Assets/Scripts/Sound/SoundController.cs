using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundController
    {
        private readonly AudioSourcePool pool;
        private readonly SoundEventChannel channel;

        public SoundController(AudioSourcePool pool, SoundConfig config)
        {
            this.pool = pool;
            channel = config.EventChannel;

            channel.OnSoundEventRaised += OnSoundPlay;
        }

        private void OnSoundPlay(SoundUnit soundUnit)
        {
            if (soundUnit == null)
                return;

            Play(soundUnit);
        }

        private AudioSource Play(SoundUnit unit)
        {
            var source = pool.GetSource();
            source.pitch = unit.Pitch;

            switch (unit.PlayType)
            {
                case PlayType.OneShot:
                    source.PlayOneShot(unit.Audio, unit.Volume);
                    return source;
            }

            source.clip = unit.Audio;
            source.volume = unit.Volume;
            source.loop = unit.PlayType == PlayType.Loop;
            source.Play();

            return source;
        }
    }
}
