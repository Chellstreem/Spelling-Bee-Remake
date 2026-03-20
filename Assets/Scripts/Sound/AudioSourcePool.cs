using System;
using UnityEngine;

namespace Sound
{
    public class AudioSourcePool
    {
        private readonly SoundConfig config;
        private readonly Transform poolHolder;
        private AudioSource[] _sources;
        private int _currentIndex;

        public AudioSourcePool(SoundConfig soundConfig)
        {
            config = soundConfig;
            poolHolder = new GameObject("Audio Source Pool Holder").transform;
            InitializePool();
        }

        private void InitializePool()
        {
            _sources = new AudioSource[config.PoolSize];

            for (int i = 0; i < config.PoolSize; i++)
            {
                GameObject gameObject = new("Audio Source");
                gameObject.transform.SetParent(poolHolder);

                var source = gameObject.AddComponent<AudioSource>();

                source.playOnAwake = false;
                source.loop = false;
                source.spatialBlend = 1f;

                _sources[i] = source;
            }
        }

        public AudioSource GetSource()
        {
            var source = _sources[_currentIndex];
            _currentIndex = (_currentIndex + 1) % _sources.Length;

            return source;
        }
    }
}