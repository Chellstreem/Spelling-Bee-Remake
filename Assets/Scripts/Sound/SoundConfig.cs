using Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Config", menuName = "Scriptable Objects/Sound/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    [Tooltip("Initial number of audio sources to allocate for sound playback")]
    [SerializeField] private int _poolSize = 10;
    [Tooltip("Maximum allowed audio sources in the pool")]
    [SerializeField] private int _maxPoolSize = 20;
    [Tooltip("Default audio channel used for raising sound events")]
    [SerializeField] private SoundEffectChannel _channel;

    public int PoolSize => _poolSize;
    public int MaxPoolSize => _maxPoolSize;
    public SoundEffectChannel Channel => _channel;
}
