using Particles;
using Zenject;
using UnityEngine;

namespace InteractableObjects
{
    public class OrbLife : InteractableObject
    {
        private ILiveRestorer restorer;
        private ISoundEffectPlayer soundEffectPlayer;
        private IParticlePlayer particlePlayer;
        
        private readonly int lifeRestored = 1;

        [Inject]
        public void Construct(ILiveRestorer restorer, ISoundEffectPlayer soundEffectPlayer, IParticlePlayer particlePlayer)
        {
            this.soundEffectPlayer = soundEffectPlayer;
            this.restorer = restorer; 
            this.particlePlayer = particlePlayer;
        }             

        protected override void OnPlayerCollision(Transform playerTransform)
        {
            restorer.RestorePlayerLives(lifeRestored);
            soundEffectPlayer.PlayEffect(SoundType.Chime);
            particlePlayer.PlayParticle(ParticleType.LifeHit, playerTransform.position);            
            Destroy(gameObject);
        }
    }
}
