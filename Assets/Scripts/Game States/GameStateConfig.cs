using GameStates;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateConfig", menuName = "Scriptable Objects/GameStateConfig")]
public class GameStateConfig : ScriptableObject
{
    [SerializeField] private GameStateDefinition[] _gameStates;

    public GameStateDefinition[] GameStates => _gameStates;
}
