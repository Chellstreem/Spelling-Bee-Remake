using Units;
using GameStates;
using UnityEngine;
using WordControl;
using Spawn;
using InputControl;

public class GameplayController
{
    private readonly GameConfig gameConfig;
    private readonly Player player;
    private readonly GameServices services;
    private readonly WordController wordController;
    private readonly GameStateController stateController;

    public GameplayController(GameConfig config, Player player, GameServices services)
    {
        gameConfig = config;
        this.player = player;
        this.services = services;
        wordController = services.Get<WordController>();
        stateController = services.Get<GameStateController>();
    }

    public void StartGame()
    {
        wordController.StartGame();
        services.Get<UnitPool>().InitializePool();

        stateController.Initialize();
        stateController.SetState(GameStateType.Countdown);
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        wordController.OnWordCompleted += OnWordCompleted;
        wordController.OnAllWordsComleted += OnAllWordsComleted;
        player.OnDeath += OnPlayerDeath;
    }

    private void OnWordCompleted()
    {
        int index = Random.Range(0, gameConfig.ActionStates.Length);
        stateController.SetState(gameConfig.ActionStates[index]);
    }

    private void OnPlayerDeath() => stateController.SetState(GameStateType.Loss);
    private void OnAllWordsComleted() => stateController.SetState(GameStateType.Victory);
}