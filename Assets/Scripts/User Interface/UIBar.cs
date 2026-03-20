using GameStates;
using UnityEngine;
using Zenject;

namespace UserInterface
{
    public abstract class UIBar : MonoBehaviour
    {
        [SerializeField] private GameStateType[] _activationStates;
        public GameStateType[] ActivationStates => _activationStates;

        [Inject]
        private void Register(UIBarController controller) => controller.Register(this);
    }
}
