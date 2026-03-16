using Units;
using UnityEngine;

namespace Animation
{
    public class InteractableUnitAnimator : MovableUnitAnimator
    {
        [Header("Interaction")]
        [SerializeField] private InteractableUnit _interactable;
        [SerializeField, Min(0.01f)] private float _deathAnimationSpeed = 1f;
        [SerializeField, Min(0.01f)] private float _attackAnimationSpeed = 1f;

        private readonly int deathTriggerHash = Animator.StringToHash("Die");
        private readonly int deathSpeedHash = Animator.StringToHash("DeathSpeed");
        private readonly int attackAnimationHash = Animator.StringToHash("Attack");

        protected override void OnEnable()
        {
            base.OnEnable();

            _animator.SetFloat(deathSpeedHash, _deathAnimationSpeed);
            _interactable.OnDeath += SetDeathAnimation;
        }

        private void SetDeathAnimation() => _animator.SetTrigger(deathTriggerHash);

        protected override void OnDisable()
        {
            base.OnDisable();
            _interactable.OnDeath -= SetDeathAnimation;
        }
    }
}