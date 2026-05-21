using UnityEngine;
using GameStateModule;
using Zenject;

namespace UIModule
{
    public class MainStageBar : SimpleBar
    {
        [Tooltip("Game states for which this bar should be active (shown)")]
        [SerializeField] private GameStateType[] _activationStates;

        public GameStateType[] ActivationStates => _activationStates;

        [Inject]
        public void Register(UIBarController controller) => controller.Register(this);
    }
}