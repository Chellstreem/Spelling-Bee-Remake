using DG.Tweening;
using UnityEngine;
using VFX;
using Zenject;

namespace UserInterface
{
    public class SimpleBar : MonoBehaviour
    {
        [Header("Activation/Deactivation")]
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private float _duration = 1f;
        protected VisualEffectServices _visualServices;

        [Inject]
        public void Construct(VisualEffectServices visualServices) => _visualServices = visualServices;

        public virtual void Activate()
        {
            if (_ease == Ease.Unset)
            {
                gameObject.SetActive(true);
                return;
            }

            _visualServices.GetService<ObjectScaler>().ActivateWithScale(transform, _duration, easeType: _ease);
        }

        public virtual void Deactivate()
        {
            if (_ease == Ease.Unset)
            {
                gameObject.SetActive(false);
                return;
            }

            _visualServices.GetService<ObjectScaler>().DeactivateWithScale(transform, _duration, easeType: _ease);
        }
    }
}
