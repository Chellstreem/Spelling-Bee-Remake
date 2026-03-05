using System.Collections;
using UnityEngine;


namespace Sounds
{
    public class SoundEffectPlayer : ISoundEffectPlayer
    {
        private readonly CoroutineRunner coroutineRunner;
        private readonly ISoundLibrary library;

        private readonly AudioSource audioSource;       

        public SoundEffectPlayer(CoroutineRunner coroutineRunner, ISoundLibrary library)
        {
            this.coroutineRunner = coroutineRunner;
            audioSource = new GameObject("SoundEffectPlayer").AddComponent<AudioSource>();
            this.library = library;
        }

        public void PlayEffect(SoundType soundType)
        {            
            AudioClip clip = library.GetClip(soundType);
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        public void PlayEffectSequence(SoundType[] soundTypes)
        {
            coroutineRunner.StartCor(SoundEffectSequenceCoroutine(soundTypes));
        }

        private IEnumerator SoundEffectSequenceCoroutine(SoundType[] soundTypes)
        {
            foreach (SoundType sound in soundTypes)
            {
                AudioClip clip = library.GetClip(sound);
                audioSource.PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length);
            }
        }        
    }
}
