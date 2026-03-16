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
        [SerializeField] private MovableUnit _movable;

        [Header("Movement Animation Speed Multipliers")]
        [SerializeField, Min(0.01f)] private float _idleSpeed = 1f;
        [SerializeField, Min(0.01f)] private float _moveSpeed = 1f;

        private readonly int moveHash = Animator.StringToHash("IsMoving");

        protected virtual void OnEnable()
        {
            SetAnimation();
            _movable.OnMovementChanged += SetAnimation;
        }

        private void SetAnimation()
        {
            _animator.SetBool(moveHash, _movable.IsMoving);
            _animator.speed = _movable.IsMoving ? _moveSpeed : _idleSpeed;
        }

        protected virtual void OnDisable() => _movable.OnMovementChanged -= SetAnimation;
    }
}
