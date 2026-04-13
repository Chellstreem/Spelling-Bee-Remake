using GameStates;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateConfig", menuName = "Scriptable Objects/GameStateConfig")]
public class GameStateConfig : ScriptableObject
{
    [Tooltip("Array of game state definitions used by the GameStateController")]
    [SerializeField] private GameStateDefinition[] _gameStates;

    public GameStateDefinition[] GameStates => _gameStates;
}
