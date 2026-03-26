using UnityEngine;

namespace VFX
{
    public class VisualEffectServices
    {
        public ParticlePlayer ParticlePlayer { get; }

        public VisualEffectServices(ParticlePlayer particlePlayer)
        {
            ParticlePlayer = particlePlayer;
        }
    }
}