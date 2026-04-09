using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class AudioSourcePool
    {
        private readonly SoundConfig config;
        private readonly Transform cameraTransform;
        private readonly List<AudioSource> sources = new();

        public AudioSourcePool(SoundConfig soundConfig, Camera camera)
        {
            config = soundConfig;
            cameraTransform = camera.transform;
            InitializePool();
        }

        public AudioSource GetSource()
        {
            foreach (var source in sources)
            {
                if (!source.isPlaying)
                    return source;
            }

            if (sources.Count >= config.MaxPoolSize)
                return sources[0];

            var newUnit = CreateSource();
            sources.Add(newUnit);
            return newUnit;
        }

        public AudioSource CreateSource()
        {
            GameObject gameObject = new("Audio Source");
            gameObject.transform.SetParent(cameraTransform);
            gameObject.transform.localPosition = Vector3.zero;

            var source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = false;

            return source;
        }

        private void InitializePool()
        {
            for (int i = 0; i < config.PoolSize; i++)
            {
                var unit = CreateSource();
                sources.Add(unit);
            }
        }
    }
}