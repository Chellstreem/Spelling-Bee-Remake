using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundPool : ISoundLibrary
    {
        private readonly Sound[] sounds;
        private Dictionary<SoundType, AudioClip> soundMap;

        public SoundPool(SoundConfig config)
        {
            sounds = config.Sounds;
            
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            soundMap = new Dictionary<SoundType, AudioClip>();

            foreach (var sound in sounds)
            {
                if (!soundMap.ContainsKey(sound.Type))
                {
                    soundMap.Add(sound.Type, sound.Clip);
                }
            }
        }

        public AudioClip GetClip(SoundType soundType)
        {
            return soundMap.TryGetValue(soundType, out var clip) ? clip : null;
        }
    }
}
