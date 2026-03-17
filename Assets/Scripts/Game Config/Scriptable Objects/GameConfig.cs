using UnityEngine;
using CameraControl;
using WordControl;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private CameraConfig _cameraConfig;
    [SerializeField] private SpawnConfig _spawnConfig;
    [SerializeField] private GameStateConfig _gameStateConfig;
    [SerializeField] private WordControlConfig _wordControlConfig;

    public CameraConfig CameraConfig => _cameraConfig;
    public SpawnConfig SpawnConfig => _spawnConfig;
    public GameStateConfig GameStateConfig => _gameStateConfig;
    public WordControlConfig WordControlConfig => _wordControlConfig;

    //

    [Header("Movement Settings")]
    [SerializeField, Min(0f)] private float _minGameSpeed = 5f;
    [SerializeField] private float _gameSpeed = 20f;
    [SerializeField] private Vector3 _moveDirection = Vector3.back;
    [SerializeField] private float _returnThreshold = 56f;
    [SerializeField] private Vector3 backgroundPosition;

    public float MinGameSpeed => _minGameSpeed;
    public float GameSpeed => _gameSpeed;
    public Vector3 MoveDirection => _moveDirection;
    public float ReturnThreshold => _returnThreshold;
    public Vector3 BackgroundPosition => backgroundPosition;

    //

    [Header("Player Config")]
    [SerializeField] private float _playerSpeed = 25f;
    [SerializeField] private float _positionTolerance = 0.001f;
    [SerializeField] private Vector3 _lowerPosition = new(-1.33f, 0, -42.74f);
    [SerializeField] private Vector3 _upperPosition = new(-1.33f, 5, -42.74f);
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private int _livesOnStart = 2;
    [SerializeField] private Vector3 _deathColliderCenter = new(0, 1.3f, 0.38f);

    public float PlayerSpeed => _playerSpeed;
    public float PlayerPositionTolerance => _positionTolerance;
    public Vector3 PlayerLowerPosition => _lowerPosition;
    public Vector3 PlayerUpperPosition => _upperPosition;
    public int PlayerMaxLives => _maxLives;
    public int PlayerStartLives => _livesOnStart;
    public Vector3 PlayerDeathColliderCenter => _deathColliderCenter;
}
