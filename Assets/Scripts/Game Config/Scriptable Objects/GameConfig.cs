using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float _gameSpeed = 20f;
    [SerializeField, Min(0f)] private float _minGameSpeed = 5f;
    [SerializeField] private Vector3 _moveDirection = Vector3.back;

    public float GameSpeed => _gameSpeed;
    public float MinGameSpeed => _minGameSpeed;
    public Vector3 MoveDirection => _moveDirection;

    //

    [Header("Movable Object Config")]
    [SerializeField] private Vector3 backgroundPosition;
    [SerializeField] private float thresholdZ;

    public Vector3 BackgroundPosition => backgroundPosition;
    public float ThresholdZ => thresholdZ;

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
