using UnityEngine;
using VFX;
using System.Collections;

namespace Units
{
    [CreateAssetMenu(fileName = "Unit Status", menuName = "Scriptable Objects/Unit Statuses/Unit Status")]
    public class UnitStatusDefinition : ScriptableObject
    {
        [SerializeField] private UnitStatusType _type;
        [SerializeField] private bool _canTakeDamage = true;
        [SerializeField] private bool _canDealDamage = true;
        [SerializeField] private bool _canMove = true;
        [SerializeField] protected ParticleEffectInfo _statusParticle;

        public UnitStatusType Type => _type;
        public bool CanTakeDamage => _canTakeDamage;
        public bool CanDealDamage => _canDealDamage;
        public bool CanMove => _canMove;

        public UnitStatus CreateStatus(ComplexUnit unit, CoroutineRunner runner) => new(unit, this, runner);

        public virtual void Enter(UnitStatus status)
        {
            if (_statusParticle.Type != ParticleType.None)
                status.StatusEffect = status.Unit.ApplyParticleEffect(_statusParticle, status.Unit.transform);

            if (status.Duration > 0)
                status.StatusCoroutine = status.CoroutineRunner.Run(StatusCoroutine(status));
        }

        public virtual void Exit(UnitStatus status)
        {
            if (status.StatusEffect != null)
            {
                status.StatusEffect.Stop();
                status.StatusEffect.gameObject.SetActive(false);
            }


            StopStatusCoroutine(status);
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