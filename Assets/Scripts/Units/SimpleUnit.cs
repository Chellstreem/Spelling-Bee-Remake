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

        public override void Damage(int damage) => AnimateAndReturn();

        public override void HandleCollision(Unit other)
        {
            other.Damage(_damage);
            AnimateAndReturn();
        }

        private void AnimateAndReturn()
        {
            _collisionSound.PlayOneShot();
            ApplyParticleEffect(_collisionEffect);
            _objectPool.ReturnObject(gameObject);
        }
    }
}