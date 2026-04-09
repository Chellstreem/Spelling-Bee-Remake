using Units;
using GameStates;
using UnityEngine;
using WordControl;
using Spawn;
using Zenject;
using Sound;

public class GameplayController
{
    private readonly GameConfig gameConfig;
    private readonly Player player;
    private readonly WordController wordController;
    private readonly GameStateController stateController;
    private readonly UnitPool unitPool;

    public GameplayController(GameConfig config, Player player, WordController wordController,
     GameStateController stateController, UnitPool unitPool)
    {
        gameConfig = config;
        this.player = player;
        this.wordController = wordController;
        this.stateController = stateController;
        this.unitPool = unitPool;
    }

    public void StartGame()
    {
        wordController.StartGame();
        unitPool.InitializePool();

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