using GameStateModule;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateConfig", menuName = "Game/Game States/Game State Config")]
public class GameStateConfig : ScriptableObject
{
    [Tooltip("Array of game state definitions used by the GameStateController")]
    [SerializeField] private GameStateDefinition[] _gameStates;

    public GameStateDefinition[] GameStates => _gameStates;
}
