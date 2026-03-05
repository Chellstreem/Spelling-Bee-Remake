using Particles;
using UnityEngine;
using Zenject;

namespace InteractableObjects
{
    public class LandCrazyMonkey : InteractableObject, IWhooshable, IEventSubscriber<OnLossStateEnter>
    {
        private EventManager eventManager;
        private IParticlePlayer particlePlayer;
        private ISoundEffectPlayer soundEffectPlayer;
        private IDamageDealer damageDealer;        
        private Animator animator;

        private readonly int damageAmount = 1;
        private readonly SoundType[] biteSequence = { SoundType.Punch, SoundType.MonkeyNoise };
        private readonly int isBitten = Animator.StringToHash("isBitten");
        private readonly int isDead = Animator.StringToHash("isDead");

        [Inject]
        public void Construct(
            EventManager eventManager, 
            IParticlePlayer particlePlayer,
            ISoundEffectPlayer soundEffectPlayer,
            IDamageDealer damageDealer)
        {
            this.eventManager = eventManager;
            this.particlePlayer = particlePlayer;
            this.soundEffectPlayer = soundEffectPlayer;
            this.damageDealer = damageDealer;
        }

        private void Awake() => animator = GetComponent<Animator>();

        private void OnEnable()
        {
            PlayMonkeyNoise();
            eventManager.Subscribe<OnLossStateEnter>(this);
        }

        protected override void OnPlayerCollision(Transform playerTransform)
        {
            particlePlayer.PlayParticle(ParticleType.MildHit, playerTransform.position);
            animator.SetTrigger(isBitten);
            damageDealer.DamagePlayer(damageAmount);
            soundEffectPlayer.PlayEffectSequence(biteSequence);
        }        

        public void OnEvent(OnLossStateEnter eventData)
        {
            animator.SetTrigger(isDead);
            PlayMonkeyNoise();
        }

        public void OnWhoosh() => PlayMonkeyNoise();
        
        private void PlayMonkeyNoise() => soundEffectPlayer.PlayEffect(SoundType.MonkeyNoise);        
    }
}
