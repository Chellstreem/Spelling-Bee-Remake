using GameStates;
using UnityEngine;
using VFX;
using Zenject;

namespace UserInterface
{
    public abstract class UIBar : MonoBehaviour
    {
        [SerializeField] private GameStateType[] _activationStates;
        protected VisualEffectServices _visualServices;

        public GameStateType[] ActivationStates => _activationStates;

        [Inject]
        public void Construct(UIBarController controller, VisualEffectServices visualServices)
        {
            _visualServices = visualServices;
            controller.Register(this);
        }

        public virtual void Activate() => gameObject.SetActive(true);
        public virtual void Deactivate() => gameObject.SetActive(false);
    }
}
