using UnityEngine;
using VFX;
using System.Collections;

namespace Units
{
    [CreateAssetMenu(fileName = "Unit Status", menuName = "Scriptable Objects/Unit Statuses/Unit Status")]
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

        public UnitStatus CreateStatus(ComplexUnit unit, CoroutineRunner runner) => new(unit, this, runner);

        public virtual void Enter(UnitStatus status)
        {
            if (_statusParticle.Type != ParticleType.None)
                status.StatusEffect = status.Unit.ApplyParticleEffect(_statusParticle, status.Unit.transform);

            if (status.Duration > 0)
                status.StatusCoroutine = status.CoroutineRunner.Run(StatusCoroutine(status));

            if (_isVisible)
                return;

            if (status.Unit is Player player)
                player.SetVisible(false);
        }

        public virtual void Exit(UnitStatus status)
        {
            if (status.StatusEffect != null)
            {
                status.StatusEffect.Stop();
                status.StatusEffect.gameObject.SetActive(false);
            }

            StopStatusCoroutine(status);

            if (status.Unit is Player player)
                player.SetVisible(true);
        }

        protected IEnumerator StatusCoroutine(UnitStatus status)
        {
            yield return new WaitForSeconds(status.Duration);
            status.StatusCoroutine = null;

            status.Unit.StatusController.SetStatus(status.Unit.DefaultStatus);
        }

        private void StopStatusCoroutine(UnitStatus status)
        {
            if (status.StatusCoroutine != null)
            {
                status.CoroutineRunner.Stop(status.StatusCoroutine);
                status.StatusCoroutine = null;
            }
        }
    }
}