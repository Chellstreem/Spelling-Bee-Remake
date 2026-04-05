using Units;
using GameStates;
using UnityEngine;

public class GameplayController
{
    private readonly GameConfig gameConfig;
    private readonly Player player;
    private readonly GameContext context;

    public GameplayController(GameConfig config, Player player, GameContext context)
    {
        gameConfig = config;
        this.player = player;
        this.context = context;
    }

    public void StartGame()
    {
        context.WordController.StartGame();
        context.UnitPool.InitializePool();
        context.Input.Enable();

        context.StateController.SetState(GameStateType.Countdown);

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        context.WordController.OnWordCompleted += OnWordCompleted;
        context.WordController.OnAllWordsComleted += OnAllWordsComleted;
        player.OnDeath += OnPlayerDeath;
    }

    private void OnWordCompleted()
    {
        int index = Random.Range(0, gameConfig.ActionStates.Length);
        context.StateController.SetState(gameConfig.ActionStates[index]);
    }

    private void OnPlayerDeath() => context.StateController.SetState(GameStateType.Loss);
    private void OnAllWordsComleted() => context.StateController.SetState(GameStateType.Victory);
}