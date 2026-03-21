using UnityEngine;

namespace Sound
{
    public class AudioSourceUnit
    {
        private readonly AudioSource source;
        private readonly AudioSourceUnitPool pool;
        public bool IsPlaying => source.isPlaying;

        public AudioSourceUnit(AudioSource source, AudioSourceUnitPool pool)
        {
            this.source = source;
            this.pool = pool;
        }

        public void Play(SoundUnit unit)
        {
            source.pitch = unit.Pitch;

            switch (unit.PlayType)
            {
                case PlayType.OneShot:
                    source.PlayOneShot(unit.Audio, unit.Volume);
                    return;
            }

            source.clip = unit.Audio;
            source.volume = unit.Volume;
            source.loop = unit.PlayType == PlayType.Loop;
            source.Play();
        }

        public void Stop()
        {
            source.Stop();
            //Release();
        }
    }
}