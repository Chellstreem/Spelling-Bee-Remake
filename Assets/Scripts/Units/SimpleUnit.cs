using Sound;
using UnityEngine;
using VFX;

namespace Units
{
    public class SimpleUnit : Unit
    {
        [Tooltip("Sound to play when this unit collides with another unit")]
        [SerializeField] private SoundUnit _collisionSound;
        [Tooltip("Damage value applied on collision")]
        [SerializeField] private int _damage = 1;
        [Tooltip("Particle effect spawned on collision")]
        [SerializeField] private ParticleEffectInfo _collisionEffect;

        [Header("Status Invoking")]
        [Tooltip("Status to apply to the other unit on collision (None = no status)")]
        [SerializeField] private UnitStatusType _collisionStatus = UnitStatusType.None;
        [Tooltip("Duration in seconds for the collision-applied status")]
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