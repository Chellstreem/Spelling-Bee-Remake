using UnityEngine;
using Zenject;

namespace GameStates
{
    public class GameState
    {
        public GameStateDefinition Definition { get; }
        public GameStateController StateController { get; private set; }
        public CoroutineRunner Runner { get; private set; }
        public GameStateType StateType { get; private set; }
        public bool AllowMoving { get; private set; }
        public bool KillInteractableObject { get; private set; }
        public Coroutine StateCoroutine { get; set; }

        public GameState(GameStateDefinition definition, GameStateController stateController, CoroutineRunner runner)
        {
            Definition = definition;
            StateController = stateController;
            Runner = runner;
            StateType = definition.StateType;
            AllowMoving = definition.AllowMoving;
            KillInteractableObject = definition.KillInteractableObject;
        }

        public void Enter() => Definition.Enter(this);
        public void Exit() => Definition.Exit(this);

        public bool AllowTransitionTo(GameStateType newStateType) => Definition.AllowTransitionTo(newStateType);
    }
}
