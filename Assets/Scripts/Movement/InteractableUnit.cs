using System;
using UnityEngine;
using VFX;
using Zenject;

namespace Movement
{
    public class InteractableUnit : MovableUnit
    {
        [SerializeField] private ParticleEffectInfo _returnEffect;
        [Inject] private ParticlePlayer _particlePlayer;

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
                _particlePlayer.Play(_returnEffect.Type, transform.position, _returnEffect.Scale);
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
