using UnityEngine;
using VFX;

namespace GameStates
{
    public class GameState
    {
        public GameStateDefinition Definition { get; }
        public GameContext Context { get; }
        public AudioSource AudioSource { get; }
        public Coroutine StateCoroutine { get; set; }

        public GameState(GameStateDefinition definition, GameContext context)
        {
            Definition = definition;
            Context = context;
            AudioSource = context.AudioSourcePool.GetSource();
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);
        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);
    }
}
