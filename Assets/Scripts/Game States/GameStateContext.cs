using Sound;
using Spawn;
using VFX;

namespace GameStates
{
    public class GameStateContext
    {
        public GameStateController StateController { get; }
        public CoroutineRunner Runner { get; }
        public Spawner Spawner { get; }
        public GameSpeedController SpeedController { get; }
        public AudioSourcePool AudioSourcePool { get; }
        public ParticlePlayer ParticlePlayer { get; }

        public GameStateContext(
            GameStateController stateController,
            CoroutineRunner runner,
            Spawner spawner,
            GameSpeedController speedController,
            AudioSourcePool audioSourcePool,
            ParticlePlayer particlePlayer)
        {
            StateController = stateController;
            Runner = runner;
            Spawner = spawner;
            SpeedController = speedController;
            AudioSourcePool = audioSourcePool;
            ParticlePlayer = particlePlayer;
        }
    }
}