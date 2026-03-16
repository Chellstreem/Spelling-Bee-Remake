using System.Collections;
using System.Collections.Generic;
using Movement;
using Units;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class MovableUnitAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        [Header("Movement")]
        [SerializeField] private MovableUnit _movable;
        [SerializeField, Min(0.01f)] private float _idleSpeed = 1f;
        [SerializeField, Min(0.01f)] private float _moveSpeed = 1f;

        private readonly int moveBoolHash = Animator.StringToHash("IsMoving");
        private readonly int idleSpeedHash = Animator.StringToHash("IdleSpeed");
        private readonly int moveSpeedHash = Animator.StringToHash("MoveSpeed");

        protected virtual void OnEnable()
        {
            SetAnimation();
            _animator.SetFloat(idleSpeedHash, _idleSpeed);
            _animator.SetFloat(moveSpeedHash, _moveSpeed);

            _movable.OnMovementChanged += SetAnimation;
        }

        private void SetAnimation()
        {
            _animator.SetBool(moveBoolHash, _movable.IsMoving);
            _animator.speed = _movable.IsMoving ? _moveSpeed : _idleSpeed;
        }

        protected virtual void OnDisable() => _movable.OnMovementChanged -= SetAnimation;
    }
}
