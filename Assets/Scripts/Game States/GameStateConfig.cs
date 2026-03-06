using GameStates;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateConfig", menuName = "Scriptable Objects/GameStateConfig")]
public class GameStateConfig : ScriptableObject
{
    [SerializeField] private GameState[] _gameStates;

    public GameState[] GameStates => _gameStates;
}
