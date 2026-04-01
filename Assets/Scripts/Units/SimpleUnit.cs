using Sound;
using UnityEngine;
using VFX;

namespace Units
{
    public class SimpleUnit : Unit
    {
        [SerializeField] private SoundUnit _collisionSound;
        [SerializeField] private int _damage = 1;
        [SerializeField] private ParticleEffectInfo _collisionEffect;

        [Header("Status Invoking")]
        [SerializeField] private UnitStatusType _collisionStatus = UnitStatusType.None;
        [SerializeField, Min(0f)] private float _statusDuration = 3f;

        public override void Damage(int damage) => AnimateAndReturn();

        public override void HandleCollision(Unit other)
        {
            other.Damage(_damage);
            AnimateAndReturn();

            if (_collisionStatus == UnitStatusType.None)
                return;

            if (other is Player player)
                player.StatusController.SetStatus(_collisionStatus, _statusDuration);
        }

        private void AnimateAndReturn()
        {
            _collisionSound.PlayOneShot();
            ApplyParticleEffect(_collisionEffect);
            _objectPool.ReturnObject(gameObject);
        }
    }
}