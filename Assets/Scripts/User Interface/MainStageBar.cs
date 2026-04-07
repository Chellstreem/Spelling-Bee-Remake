using UnityEngine;
using GameStates;
using Zenject;

namespace UserInterface
{
    public class MainStageBar : SimpleBar
    {
        [SerializeField] private GameStateType[] _activationStates;

        public GameStateType[] ActivationStates => _activationStates;

        [Inject]
        public void Register(UIBarController controller) => controller.Register(this);
    }
}