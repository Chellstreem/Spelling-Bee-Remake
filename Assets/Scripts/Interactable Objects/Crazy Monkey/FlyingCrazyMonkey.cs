using Particles;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public class FlyingCrazyMonkey : InteractableObject, IWhooshable
    {
        private IParticlePlayer particlePlayer;
        private ISoundEffectPlayer soundEffectPlayer;
        private IDamageDealer damageDealer;
        private ISpawnableObjectReturner returner;        
        
        private readonly int damageAmount = 1;        

        [Inject]
        public void Construct(            
            IParticlePlayer particlePlayer,
            ISoundEffectPlayer soundEffectPlayer,
            IDamageDealer damageDealer,
            ISpawnableObjectReturner returner)
        {            
            this.particlePlayer = particlePlayer;
            this.soundEffectPlayer = soundEffectPlayer;
            this.damageDealer = damageDealer;
            this.returner = returner;
        }        

        private void OnEnable() => PlayMonkeyNoise();        

        protected override void OnPlayerCollision(Transform playerTransform)
        {
            particlePlayer.PlayParticle(ParticleType.MissileExplosion, transform.position);
            damageDealer.DamagePlayer(damageAmount);
            soundEffectPlayer.PlayEffect(SoundType.MissileExplosion);
            returner.ReturnObject(gameObject);
        }        

        public void OnWhoosh() => PlayMonkeyNoise();

        private void PlayMonkeyNoise() => soundEffectPlayer.PlayEffect(SoundType.MonkeyNoise);
    }
}

