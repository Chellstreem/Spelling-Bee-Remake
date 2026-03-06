using System.Collections.Generic;
using Zenject;

namespace GameStates.Moving
{
    public class SubstateInitializer : ISubstateInitializer<MovingStateSubstate>
    {
        private readonly DiContainer container;
        private Dictionary<MovingStateSubstate, GameState> subStates;

        public SubstateInitializer(DiContainer container)
        {
            this.container = container;
        }

        public void InitializeSubstates()
        {
            subStates = new Dictionary<MovingStateSubstate, GameState>
        {
            { MovingStateSubstate.Safe, container.Resolve<SafeSubstate>() },
            { MovingStateSubstate.Interactive, container.Resolve<InteractiveSubstate>() },
            { MovingStateSubstate.Missile, container.Resolve<MissileSubstate>() }
        };
        }

        public GameState GetSubstate(MovingStateSubstate state)
        {
            return subStates.TryGetValue(state, out var subState) ? subState : null;
        }
    }
}
