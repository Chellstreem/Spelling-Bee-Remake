using Units;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(ComplexUnit))]
    public class ComplexUnitAnimator : MovableUnitAnimator
    {
        [SerializeField, Min(0.01f)] private float _deathAnimationSpeed = 1f;
        [SerializeField, Min(0.01f)] private float _attackAnimationSpeed = 1f;
        [SerializeField, Min(0.01f)] private float _danceAnimationSpeed = 1f;
        private ComplexUnit _interactable;

        private readonly int deathTriggerHash = Animator.StringToHash("Die");
        private readonly int deathSpeedHash = Animator.StringToHash("DeathSpeed");
        private readonly int attackTriggerHash = Animator.StringToHash("Attack");
        private readonly int attackSpeedHash = Animator.StringToHash("AttackSpeed");
        private readonly int danceTriggerHash = Animator.StringToHash("Dance");
        private readonly int danceSpeedHash = Animator.StringToHash("DanceSpeed");

        protected override void Awake()
        {
            base.Awake();

            _interactable = GetComponent<ComplexUnit>();

            _animator.SetFloat(deathSpeedHash, _deathAnimationSpeed);
            _animator.SetFloat(attackSpeedHash, _attackAnimationSpeed);
            _animator.SetFloat(danceSpeedHash, _danceAnimationSpeed);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _interactable.OnDeath += SetDeathAnimation;
            _interactable.OnAttack += SetAttackAnimation;
            _interactable.OnDance += SetDanceAnimation;
        }

        private void SetDeathAnimation() => _animator.SetTrigger(deathTriggerHash);
        private void SetAttackAnimation() => _animator.SetTrigger(attackTriggerHash);
        private void SetDanceAnimation() => _animator.SetTrigger(danceTriggerHash);

        protected override void OnDisable()
        {
            base.OnDisable();
            _interactable.OnDeath -= SetDeathAnimation;
            _interactable.OnAttack -= SetAttackAnimation;
            _interactable.OnDance -= SetDanceAnimation;
        }
    }
}