using Sound;
using Spawn;
using VFX;
using GameStates;
using WordControl;
using InputControl;

public class GameContext
{
    public GameStateController StateController { get; }
    public CoroutineRunner Runner { get; }
    public UnitPool UnitPool { get; }
    public Spawner Spawner { get; }
    public GameSpeedController SpeedController { get; }
    public AudioSourcePool AudioSourcePool { get; }
    public ParticlePlayer ParticlePlayer { get; }
    public WordController WordController { get; }
    public IInput Input { get; }

    public GameContext(GameStateController stateController, CoroutineRunner runner, UnitPool unitPool,
        Spawner spawner, GameSpeedController speedController, AudioSourcePool audioSourcePool,
        ParticlePlayer particlePlayer, WordController wordController, IInput input)
    {
        StateController = stateController;
        Runner = runner;
        UnitPool = unitPool;
        Spawner = spawner;
        SpeedController = speedController;
        AudioSourcePool = audioSourcePool;
        ParticlePlayer = particlePlayer;
        WordController = wordController;
        Input = input;
    }
}
