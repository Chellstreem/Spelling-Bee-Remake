using UnityEngine;

[CreateAssetMenu(fileName = "ParticleConfig", menuName = "Scriptable Objects/Particle/ParticleConfig")]
public class ParticleConfig : ScriptableObject
{
    [SerializeField] private ParticleObject[] particleObjects;
    [SerializeField] private float confettiRainOffsetY = 14f;
    [SerializeField] private float soulEscapeDelay = 1.5f;
    [SerializeField] private float soulEscapeOffsetY = 2f;
    [SerializeField] private float poofOffsetX = -1f;

    public ParticleObject[] ParticleObjects => particleObjects;
    public float ConfettiRainOffsetY => confettiRainOffsetY;
    public float SoulEscapeDelay => soulEscapeDelay;
    public float SoulEscapeOffsetY => soulEscapeOffsetY;
    public float PoofOffsetX => poofOffsetX;
}
