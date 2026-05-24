using UnityEngine;
using VFXModule;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Units
{
    [CreateAssetMenu(fileName = "Unit Status", menuName = "Game/Unit Statuses/Unit Status")]
    public class UnitStatusDefinition : ScriptableObject
    {
        [Tooltip("Status type identifier")]
        [SerializeField] private UnitStatusType _type;

        [Tooltip("Whether units in this status can receive damage")]
        [SerializeField] private bool _canTakeDamage = true;

        [Tooltip("Whether units in this status can deal damage")]
        [SerializeField] private bool _canDealDamage = true;

        [Tooltip("Whether units in this status are allowed to move")]
        [SerializeField] private bool _canMove = true;

        [Tooltip("Whether units in this status are visible")]
        [SerializeField] private bool _isVisible = true;

        [Tooltip("Optional particle effect played while unit has this status")]
        [SerializeField] protected ParticleEffectInfo _statusParticle;

        public UnitStatusType Type => _type;
        public bool CanTakeDamage => _canTakeDamage;
        public bool CanDealDamage => _canDealDamage;
        public bool CanMove => _canMove;
        public bool IsVisible => _isVisible;

        public UnitStatus CreateStatus(ComplexUnit unit) => new(unit, this);

        public virtual void Enter(UnitStatus status)
        {
            if (_statusParticle.Type != ParticleType.None)
                status.StatusEffect = status.Unit.ApplyParticleEffect(_statusParticle, status.Unit.transform);

            if (status.Duration > 0)
            {
                status.StatusCTS = new CancellationTokenSource();
                RunStatusAsync(status, status.StatusCTS.Token).Forget();
            }

            if (!_isVisible && status.Unit is Player player)
                player.SetVisible(false);
        }

        public virtual void Exit(UnitStatus status)
        {
            if (status.StatusEffect != null)
            {
                status.StatusEffect.Stop();
                status.StatusEffect.gameObject.SetActive(false);
            }

            StopStatus(status);

            if (status.Unit is Player player)
                player.SetVisible(true);
        }

        protected async UniTaskVoid RunStatusAsync(UnitStatus status, CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(Mathf.RoundToInt(status.Duration * 1000), cancellationToken: cancellationToken);
                status.Unit.StatusController.SetStatus(status.Unit.DefaultStatus);
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void StopStatus(UnitStatus status)
        {
            if (status.StatusCTS == null)
                return;

            status.StatusCTS.Cancel();
            status.StatusCTS.Dispose();
            status.StatusCTS = null;
        }
    }
}