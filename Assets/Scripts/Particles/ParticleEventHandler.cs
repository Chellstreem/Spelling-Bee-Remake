using UnityEngine;
using Zenject;

namespace Particles
{
    public class ParticleEventHandler : IEventSubscriber<OnLetterChecked>, IEventSubscriber<OnWordCompleted>,
        IEventSubscriber<OnVictory>, IEventSubscriber<OnLossStateEnter>
    {
        private readonly EventManager eventManager;
        private readonly IParticlePlayer player;        
        private readonly Transform playerTransform;
        private readonly float confettiRainOffsetY;
        private readonly float soulEscapeDelay;
        private readonly float soulEscapeOffsetY;
        private readonly float poofOffsetX;

        public ParticleEventHandler(EventManager eventManager, IParticlePlayer player,
            [Inject(Id = InstantiatedObjectType.Player)] Transform playerTransform,
            ParticleConfig config)
        {
            this.eventManager = eventManager;
            this.player = player;            
            this.playerTransform = playerTransform;
            confettiRainOffsetY = config.ConfettiRainOffsetY;
            soulEscapeDelay = config.SoulEscapeDelay;
            soulEscapeOffsetY = config.SoulEscapeOffsetY;
            poofOffsetX = config.PoofOffsetX;

            SubscribeToEvents();
        }

        public void OnEvent(OnLetterChecked eventData)
        {
            if (eventData.IsCorrect)
                player.PlayParticle(ParticleType.ArcadeSpark, eventData.Position);
            else
                player.PlayParticle(ParticleType.WrongText, eventData.Position);
                player.PlayParticle(ParticleType.Poof, eventData.Position + new Vector3(poofOffsetX, 0, 0));
        }

        public void OnEvent(OnWordCompleted eventData)
        {
            player.PlayParticle(ParticleType.BirthdayConfetti, playerTransform.position);
        }

        public void OnEvent(OnVictory eventData)
        {            
            Vector3 offset = new Vector3(0, confettiRainOffsetY, 0);
            player.PlayParticle(ParticleType.ConfettiRain, playerTransform.position + offset);
        }

        public void OnEvent(OnLossStateEnter eventData)
        {
            Vector3 offset = new Vector3(0, soulEscapeOffsetY, 0);
            player.PlayParticle(ParticleType.SoulEscape, playerTransform.position + offset, soulEscapeDelay);
        }        

        private void SubscribeToEvents()
        {
            eventManager.Subscribe<OnLetterChecked>(this);
            eventManager.Subscribe<OnWordCompleted>(this);
            eventManager.Subscribe<OnVictory>(this);
            eventManager.Subscribe<OnLossStateEnter>(this);            
        }        
    }
}
