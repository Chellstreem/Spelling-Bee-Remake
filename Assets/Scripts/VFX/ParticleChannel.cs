using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VFX
{
    [CreateAssetMenu(fileName = "Particle Channel", menuName = "Scriptable Objects/VFX/Particle Channel")]
    public class ParticleChannel : ScriptableObject
    {
        public event Action<ParticleEffect, Vector3> OnParticleInvoked;

        public void Invoke(ParticleEffect effect, Vector3 position)
        {
            OnParticleInvoked?.Invoke(effect, position);
        }
    }
}