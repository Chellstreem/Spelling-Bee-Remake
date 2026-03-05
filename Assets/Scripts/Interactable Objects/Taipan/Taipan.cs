using Particles;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public class Taipan : InteractableObject, IWhooshable
    {
        private IParticlePlayer particlePlayer;
        private ISoundEffectPlayer soundEffectPlayer;
        private IDamageDealer damageDealer;
        private Animator animator;

        private readonly int damageAmount = 1;
        private readonly SoundType[] biteSequence = { SoundType.Punch, SoundType.SnakeRattle };
        private readonly int isBitten = Animator.StringToHash("isBitten");        

        [Inject]
        public void Construct(IParticlePlayer particlePlayer, ISoundEffectPlayer soundEffectPlayer, IDamageDealer damageDealer)
        {      
            this.particlePlayer = particlePlayer;
            this.soundEffectPlayer = soundEffectPlayer;
            this.damageDealer = damageDealer;
        }

        private void Awake() => animator = GetComponent<Animator>();        

        protected override void OnPlayerCollision(Transform playerTransform)
        {
            particlePlayer.PlayParticle(ParticleType.MildHit, playerTransform.position);
            animator.SetTrigger(isBitten);
            damageDealer.DamagePlayer(damageAmount);            
            soundEffectPlayer.PlayEffectSequence(biteSequence);
        }

        public void OnWhoosh()
        {
            soundEffectPlayer.PlayEffect(SoundType.SnakeRattle);
        }
    }
}
