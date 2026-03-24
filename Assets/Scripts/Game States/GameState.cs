using Sound;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class GameState
    {
        public GameStateDefinition Definition { get; }
        public GameStateController StateController { get; }
        public CoroutineRunner Runner { get; }
        public AudioSource AudioSource { get; }
        public GameStateType StateType { get; }
        public bool AllowMoving { get; }
        public bool KillInteractableObject { get; }
        public Coroutine StateCoroutine { get; set; }


        public GameState(GameStateDefinition definition, GameStateController stateController,
         CoroutineRunner runner, AudioSource audioSource)
        {
            Definition = definition;
            StateController = stateController;
            Runner = runner;
            AudioSource = audioSource;
            StateType = definition.StateType;
            AllowMoving = definition.AllowMoving;
            KillInteractableObject = definition.KillInteractableObject;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);

        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);
    }
}
