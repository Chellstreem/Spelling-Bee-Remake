using Units;
using UnityEngine;

namespace Animation
{
    public class InteractableUnitAnimator : MovableUnitAnimator
    {
        [SerializeField] private InteractableUnit _interactable;

        [Header("Interaction Animation Speed Multipliers")]
        [SerializeField, Min(0.01f)] private float _deathAnimationSpeed = 1f;

        private readonly int dieTrigger = Animator.StringToHash("Die");
        private readonly int dieSpeed = Animator.StringToHash("DeathSpeed");

        protected override void OnEnable()
        {
            base.OnEnable();

            _animator.SetFloat(dieSpeed, _deathAnimationSpeed);
            _interactable.OnDeath += SetDeathAnimation;
        }

        private void SetDeathAnimation() => _animator.SetTrigger(dieTrigger);

        protected override void OnDisable()
        {
            base.OnDisable();
            _interactable.OnDeath -= SetDeathAnimation;
        }
    }
}