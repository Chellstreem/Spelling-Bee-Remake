using Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Config", menuName = "Scriptable Objects/Sound/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private int _maxPoolSize = 20;
    [SerializeField] private SoundEventChannel _eventChannel;

    public int PoolSize => _poolSize;
    public int MaxPoolSize => _maxPoolSize;
    public SoundEventChannel EventChannel => _eventChannel;
}
