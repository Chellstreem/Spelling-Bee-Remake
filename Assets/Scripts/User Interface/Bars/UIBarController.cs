using System.Threading;
using System.Collections.Generic;
using System.Linq;
using GameStates;
using UnityEngine;

namespace UserInterface
{
    public class UIBarController
    {
        private readonly GameStateController stateController;
        [SerializeField] private List<UIBar> _uIBars = new();

        public UIBarController(GameStateController stateController)
        {
            this.stateController = stateController;
            stateController.OnStateChanged += OnStateChanged;
        }

        public void Register(UIBar bar) => _uIBars.Add(bar);

        private void OnStateChanged()
        {
            var type = stateController.CurrentState.Definition.StateType;

            foreach (var bar in _uIBars)
            {
                bool shouldBeActive = bar.ActivationStates.Contains(type);
                bool isActive = bar.gameObject.activeSelf;

                if (shouldBeActive == isActive)
                    continue;

                if (shouldBeActive)
                    bar.Activate();
                else
                    bar.Deactivate();
            }
        }

        public void Dispose() => stateController.OnStateChanged -= OnStateChanged;
    }
}