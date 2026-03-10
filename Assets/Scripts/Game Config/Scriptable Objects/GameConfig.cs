using UnityEngine;
using CameraControl;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private CameraConfig _cameraConfig;
    [SerializeField] private SpawnConfig _spawnConfig;
    [SerializeField] private GameStateConfig _gameStateConfig;

    public CameraConfig CameraConfig => _cameraConfig;
    public SpawnConfig SpawnConfig => _spawnConfig;
    public GameStateConfig GameStateConfig => _gameStateConfig;

    //

    [Header("Movement Settings")]
    [SerializeField] private float _gameSpeed = 20f;
    [SerializeField, Min(0f)] private float _minGameSpeed = 5f;
    [SerializeField] private Vector3 _moveDirection = Vector3.back;
    [SerializeField] private float _returnThreshold = 56f;
    [SerializeField] private Vector3 backgroundPosition;

    public float GameSpeed => _gameSpeed;
    public float MinGameSpeed => _minGameSpeed;
    public Vector3 MoveDirection => _moveDirection;
    public float ReturnThreshold => _returnThreshold;
    public Vector3 BackgroundPosition => backgroundPosition;

    //

    [Header("Random Letter Config")]
    [SerializeField] private int extraLettersCount = 5;
    [SerializeField] private string alphabetLetters = "abcdefghijklmnopqrstuvwxyz";

    public int ExtraLettersCount => extraLettersCount;
    public string AlphabetLetters => alphabetLetters;

    //

    [Header("Missile Config")]
    [SerializeField] private float missileSpeed = 30f;
    [SerializeField] private float missileSpawnFrequency = 0.5f;
    [SerializeField] private float delayBeforeLaunching = 2f;
    [SerializeField] private float missileSateDuration = 3.5f;

    public float MissileSpeed => missileSpeed;
    public float MissileSpawnFrequency => missileSpawnFrequency;
    public float DelayBeforeLaunching => delayBeforeLaunching;
    public float MissileSateDuration => missileSateDuration;
}
