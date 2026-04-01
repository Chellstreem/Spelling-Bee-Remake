using Units;
using GameStates;
using WordControl;
using UnityEngine;

public class GameplayController
{
    private readonly GameConfig gameConfig;
    private readonly WordController wordController;
    private readonly GameStateController stateController;

    public GameplayController(GameConfig config, Player player, WordController wordController, GameStateController stateController)
    {
        gameConfig = config;
        this.wordController = wordController;
        this.stateController = stateController;

        wordController.OnWordCompleted += OnWordCompleted;
        player.OnDeath += OnPlayerDeath;
        wordController.OnAllWordsComleted += OnAllWordsComleted;
    }

    private void OnWordCompleted()
    {
        int index = Random.Range(0, gameConfig.ActionStates.Length);
        stateController.SetState(gameConfig.ActionStates[index]);
    }

    private void OnPlayerDeath() => stateController.SetState(GameStateType.Loss);
    private void OnAllWordsComleted() => stateController.SetState(GameStateType.Victory);
}