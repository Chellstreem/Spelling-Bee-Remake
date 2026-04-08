using Sound;
using UnityEngine;

namespace GameStates
{
    public class GameState
    {
        private AudioSource _currentSource;
        public GameStateDefinition Definition { get; }
        public GameServices GameServices { get; }
        public AudioSourcePool AudioSourcePool { get; }
        public Coroutine StateCoroutine { get; set; }

        public GameState(GameStateDefinition definition, GameServices context)
        {
            Definition = definition;
            GameServices = context;
            AudioSourcePool = context.Get<AudioSourcePool>();
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);

        public void PlaySound(SoundUnit unit, bool isLoop)
        {
            _currentSource = AudioSourcePool.GetSource();
            unit.Play(_currentSource, isLoop);
        }

        public void StopSound() => _currentSource?.Stop();
    }
}
