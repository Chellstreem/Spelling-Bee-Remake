using Particles;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public class LaunchedMissile : InteractableObject, IWhooshable
    {        
        private IDamageDealer damageDealer;
        private ISoundEffectPlayer effectPlayer;
        private IParticlePlayer particlePlayer;
        private ISpawnableObjectReturner returner;

        private readonly int damageAmount = 1;

        [Inject]
        public void Construct(IDamageDealer damageDealer, IParticlePlayer particlePlayer, ISoundEffectPlayer effectPlayer, ISpawnableObjectReturner returner)
        {
            this.effectPlayer = effectPlayer;
            this.particlePlayer = particlePlayer;
            this.damageDealer = damageDealer;
            this.returner = returner;
        }

        protected override void OnPlayerCollision(Transform playerTransform)
        {            
            damageDealer.DamagePlayer(damageAmount);
            particlePlayer.PlayParticle(ParticleType.MissileExplosion, transform.position); 
            effectPlayer.PlayEffect(SoundType.MissileExplosion);
            returner.ReturnObject(gameObject);            
        }
        
        public void OnWhoosh()
        {
            effectPlayer.PlayEffect(SoundType.Whoosh);            
        }
    }
}
