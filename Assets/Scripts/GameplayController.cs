using Units;
using GameStates;
using WordControl;

public class GameplayController
{
    private readonly Player player;
    private readonly WordController wordController;
    private readonly GameStateController stateController;

    public GameplayController(Player player, WordController wordController, GameStateController stateController)
    {
        this.player = player;
        this.wordController = wordController;
        this.stateController = stateController;

        wordController.OnWordCompleted += OnWordCompleted;
        player.OnDeath += OnPlayerDeath;
        wordController.OnAllWordsComleted += OnAllWordsComleted;
    }

    private void OnWordCompleted() => stateController.SetState(GameStateType.Missile);
    private void OnPlayerDeath() => stateController.SetState(GameStateType.Loss);
    private void OnAllWordsComleted() => stateController.SetState(GameStateType.Victory);
}