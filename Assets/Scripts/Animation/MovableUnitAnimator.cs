using Movement;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MovableUnit))]
    public class MovableUnitAnimator : MonoBehaviour
    {
        [Header("Speed Settings")]
        [Tooltip("Playback speed multiplier for idle animation")]
        [SerializeField, Min(0.01f)] private float _idleSpeed = 1f;
        [Tooltip("Playback speed multiplier for move animation")]
        [SerializeField, Min(0.01f)] private float _moveSpeed = 1f;

        protected MovableUnit _unit;
        protected Animator _animator;

        private readonly int moveBoolHash = Animator.StringToHash("IsMoving");
        private readonly int idleSpeedHash = Animator.StringToHash("IdleSpeed");
        private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");

        protected virtual void Awake()
        {
            _unit = GetComponent<MovableUnit>();
            _animator = GetComponent<Animator>();

            _animator.SetFloat(idleSpeedHash, _idleSpeed);
            _animator.SetFloat(moveSpeedHash, _moveSpeed);
        }

        protected virtual void OnEnable()
        {
            SetAnimation();
            _unit.OnMovementChanged += SetAnimation;
        }

        private void SetAnimation() => _animator.SetBool(moveBoolHash, _unit.IsMoving);
        protected virtual void OnDisable() => _unit.OnMovementChanged -= SetAnimation;
    }
}
