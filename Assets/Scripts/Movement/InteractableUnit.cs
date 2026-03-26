using System;
using UnityEngine;
using VFX;

namespace Movement
{
    public class InteractableUnit : MovableUnit
    {
        [SerializeField] private ParticleEffect _returnEffect;

        private void OnEnable()
        {
            if (_stateController.CurrentState != null && _stateController.CurrentState.AllowMoving)
                StartMoving();

            _stateController.OnStateChanged += OnStateChanged;
        }

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            if (_stateController.CurrentState.KillInteractableObject)
            {
                if (_returnEffect != null)
                    _returnEffect.Invoke(transform.position);

                _pool.ReturnObject(gameObject);
                return;
            }
        }

        private void OnDisable()
        {
            StopMoving();
            _stateController.OnStateChanged -= OnStateChanged;
        }
    }
}
