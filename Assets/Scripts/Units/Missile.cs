using Sound;
using UnityEngine;
using VFX;

namespace Units
{
    public class Missile : Unit
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private SoundUnit _collisionSound;


        [Header("VFX")]
        [SerializeField] private ParticleEffectInfo _explosionEffect;

        public override void Damage(int damage) => Explode();

        public override void HandleCollision(Unit other)
        {
            other.Damage(_damage);
            Explode();
        }

        private void Explode()
        {
            _collisionSound.PlayOneShot();
            ApplyParticleEffect(_explosionEffect);
            _objectPool.ReturnObject(gameObject);
        }
    }
}