using Sound;
using UnityEngine;
using VFX;

namespace Units
{
    public class SimpleUnit : Unit
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private UnitType[] _allowedCollisions;
        [SerializeField] private SoundUnit _collisionSound;

        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _effect;

        public override void Damage(int damage) => Animate();

        public override void HandleCollision(Unit other)
        {
            foreach (var type in _allowedCollisions)
            {
                if (type == other.UnitType)
                {
                    other.Damage(_damage);
                    Animate();
                    return;
                }
            }
        }

        private void Animate()
        {
            _collisionSound.PlayOneShot();
            ApplyParticleEffect(_effect);
            _objectPool.ReturnObject(gameObject);
        }
    }
}