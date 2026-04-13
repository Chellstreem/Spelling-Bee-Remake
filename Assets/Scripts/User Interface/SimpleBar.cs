using DG.Tweening;
using UnityEngine;
using VFX;
using Zenject;

namespace UserInterface
{
    public class SimpleBar : MonoBehaviour
    {
        [Header("Activation/Deactivation")]
        [Tooltip("Easing type applied to activation/deactivation animations")]
        [SerializeField] private Ease _ease = Ease.Linear;
        [Tooltip("Duration in seconds for activation/deactivation animations")]
        [SerializeField] private float _duration = 1f;
        protected ObjectScaler _scaler;

        [Inject]
        public void Construct(ObjectScaler scaler) => _scaler = scaler;

        public virtual void Activate()
        {
            if (_ease == Ease.Unset)
            {
                gameObject.SetActive(true);
                return;
            }

            _scaler.ActivateWithScale(transform, _duration, easeType: _ease);
        }

        public virtual void Deactivate()
        {
            if (_ease == Ease.Unset)
            {
                gameObject.SetActive(false);
                return;
            }

            _scaler.DeactivateWithScale(transform, _duration, easeType: _ease);
        }
    }
}
