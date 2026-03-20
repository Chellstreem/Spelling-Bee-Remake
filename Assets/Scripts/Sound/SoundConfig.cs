using UnityEngine;

[CreateAssetMenu(fileName = "Sound Config", menuName = "Scriptable Objects/Sound/SoundConfig")]
public class SoundConfig : ScriptableObject
{
    [SerializeField] private int _poolSize = 10;

    public int PoolSize => _poolSize;
}
