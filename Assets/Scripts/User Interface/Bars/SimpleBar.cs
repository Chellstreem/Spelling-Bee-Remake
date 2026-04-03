using DG.Tweening;
using UnityEngine;
using VFX;

namespace UserInterface
{
    public class SimpleBar : UIBar
    {
        [Header("Activation/Deactivation")]
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Ease _ease = Ease.Linear;

        public override void Activate()
        {
            _visualServices.GetService<ObjectScaler>().ActivateWithScale(transform, _duration, easeType: _ease);
        }

        public override void Deactivate()
        {
            _visualServices.GetService<ObjectScaler>().DeactivateWithScale(transform, _duration, easeType: _ease);
        }
    }
}