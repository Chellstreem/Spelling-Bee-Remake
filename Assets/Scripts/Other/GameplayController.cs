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
    private readonly WordController wordController;
    private readonly GameServices context;
    private readonly GameStateController stateController;

    public GameplayController(GameConfig config, Player player, GameServices context)
    {
        gameConfig = config;
        this.player = player;
        wordController = context.Get<WordController>();
        this.context = context;
        stateController = context.Get<GameStateController>();
    }

    public void StartGame()
    {
        wordController.StartGame();
        context.Get<UnitPool>().InitializePool();
        context.Get<IInput>().Enable();

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