using System;

namespace Sounds
{
    public class SoundEffectHandler : IEventSubscriber<OnLetterChecked>, IEventSubscriber<OnLossStateEnter>,
        IEventSubscriber<OnMissileStateEnter>, IEventSubscriber<OnCountdownTick>,
        IEventSubscriber<OnWordCompleted>
    {
        private readonly EventManager eventManager;
        private readonly ISoundEffectPlayer player; 
        private readonly IScreenObjectOnTargetPositionInformer screenObjectOnTargetPositionInformer;

        public SoundEffectHandler(EventManager eventManager, ISoundEffectPlayer soundEffectPlayer,
            IScreenObjectOnTargetPositionInformer screenObjectOnTargetPositionInformer)
        {
            this.eventManager = eventManager;
            player = soundEffectPlayer; 
            this.screenObjectOnTargetPositionInformer = screenObjectOnTargetPositionInformer;

            SubscribeToEvents();
        }        

        public void OnEvent(OnLetterChecked eventData)
        {
            if (eventData.IsCorrect)
                player.PlayEffect(SoundType.Pick);
            else
                player.PlayEffect(SoundType.Buzzer);
        }

        public void OnEvent(OnLossStateEnter eventData)
        {
            SoundType type = UnityEngine.Random.value > 0.5 ? SoundType.Willhelm : SoundType.TomScream;
            player.PlayEffect(type);
            player.PlayEffect(SoundType.SadTrombone);
        }

        public void OnEvent(OnMissileStateEnter eventData) => player.PlayEffect(SoundType.MissileLockOn);

        public void OnEvent(OnWordCompleted eventData)
        {
            if (eventData.GameplayAction == GameplayActionType.CameraShake)
                player.PlayEffect(SoundType.Earthquake);
        }

        public void OnEvent(OnCountdownTick eventData)
        {
            if (eventData.Count != 0)
                player.PlayEffect(SoundType.Tick);
            else
                player.PlayEffect(SoundType.Go);
        }  
        
        public void OnScreenObjectShown()
        {
            SoundType type = UnityEngine.Random.value > 0.5 ? SoundType.OMG: SoundType.Huh;
            player.PlayEffect(type);
        }

        private void SubscribeToEvents()
        {
            eventManager.Subscribe<OnCountdownTick>(this);            
            eventManager.Subscribe<OnLetterChecked>(this);
            eventManager.Subscribe<OnLossStateEnter>(this);
            eventManager.Subscribe<OnMissileStateEnter>(this);
            eventManager.Subscribe<OnWordCompleted>(this);
            screenObjectOnTargetPositionInformer.OnReachingTargetPosition += OnScreenObjectShown;
        }        
    }
}
