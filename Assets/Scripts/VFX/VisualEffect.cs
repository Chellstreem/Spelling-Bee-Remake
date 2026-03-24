using UnityEngine;

namespace VFX
{
    public abstract class VisualEffect : ScriptableObject
    {
        public abstract void Play(Vector3 position, VisualEffectContext context);
    }
}
