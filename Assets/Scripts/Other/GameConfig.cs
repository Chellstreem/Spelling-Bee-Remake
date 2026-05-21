using UnityEngine;
using CameraModule;
using WordControlModule;
using VFXModule;
using Units;
using GameStateModule;
using SceneControl;
using BackgroundControl;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Tooltip("Collection of scene assets and their load paths")]
    [SerializeField] private SceneCollection _sceneCollection;
    [Tooltip("Background configuration used to select background visuals")]
    [SerializeField] private BackgroundConfig _backgroundConfig;

    public SceneCollection SceneCollection => _sceneCollection;
    public BackgroundConfig BackgroundConfig => _backgroundConfig;

    //

    [Header("Movement Settings")]
    [Tooltip("Minimum allowed game speed")]
    [SerializeField, Min(0f)] private float _minGameSpeed = 5f;
    [Tooltip("Base game speed used for object movement")]
    [SerializeField] private float _gameSpeed = 20f;
    [Tooltip("Direction vector applied to world movement")]
    [SerializeField] private Vector3 _moveDirection = Vector3.back;
    [Tooltip("Distance threshold at which moving objects return/reset")]
    [SerializeField] private float _returnThreshold = 56f;
    [Tooltip("Default background placement position")]
    [SerializeField] private Vector3 backgroundPosition;

    public float MinGameSpeed => _minGameSpeed;
    public float GameSpeed => _gameSpeed;
    public Vector3 MoveDirection => _moveDirection;
    public float ReturnThreshold => _returnThreshold;
    public Vector3 BackgroundPosition => backgroundPosition;

    //

    [Header("Player Config")]
    [Tooltip("Player horizontal movement speed")]
    [SerializeField] private float _playerSpeed = 25f;
    [Tooltip("Tolerance used when comparing positions for movement completion")]
    [SerializeField] private float _positionTolerance = 0.001f;
    [Tooltip("Lower bound position the player can move to")]
    [SerializeField] private Vector3 _lowerPosition = new(-1.33f, 0, -42.74f);
    [Tooltip("Upper bound position the player can move to")]
    [SerializeField] private Vector3 _upperPosition = new(-1.33f, 5, -42.74f);
    [Tooltip("Maximum number of lives the player can have")]
    [SerializeField] private int _maxLives = 3;
    [Tooltip("Number of lives the player has at the start of a game")]
    [SerializeField] private int _livesOnStart = 2;
    [Tooltip("Collider center offset applied when the player dies to enable death physics")]
    [SerializeField] private Vector3 _deathColliderCenter = new(0, 1.3f, 0.38f);

    public float PlayerSpeed => _playerSpeed;
    public float PlayerPositionTolerance => _positionTolerance;
    public Vector3 PlayerLowerPosition => _lowerPosition;
    public Vector3 PlayerUpperPosition => _upperPosition;
    public int PlayerMaxLives => _maxLives;
    public int PlayerStartLives => _livesOnStart;
    public Vector3 PlayerDeathColliderCenter => _deathColliderCenter;

    //

    [Header("Other")]
    [Tooltip("GameState types that allow player actions during play (used by UI/Rules)")]
    [SerializeField] private GameStateType[] _actionStates;
    [Tooltip("Text displayed when player completes all words")]
    [SerializeField] private string _victoryText = "YOU DID IT!";

    public GameStateType[] ActionStates => _actionStates;
    public string VictoryText => _victoryText;
}
