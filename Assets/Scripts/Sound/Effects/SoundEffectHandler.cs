using System;
using GameStates;

namespace Sounds
{
    public class SoundEffectHandler : IEventSubscriber<OnLetterChecked>,
        IEventSubscriber<OnMissileStateEnter>, IEventSubscriber<OnCountdownTick>,
        IEventSubscriber<OnWordCompleted>
    {
        private readonly GameStateController _stateController;
        private readonly EventManager eventManager;
        private readonly ISoundEffectPlayer player;
        private readonly IScreenObjectOnTargetPositionInformer screenObjectOnTargetPositionInformer;

        public SoundEffectHandler(GameStateController stateController, EventManager eventManager, ISoundEffectPlayer soundEffectPlayer,
            IScreenObjectOnTargetPositionInformer screenObjectOnTargetPositionInformer)
        {
            _stateController = stateController;
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

        private void OnStateChanged()
        {
            switch (_stateController.CurrentState.StateType)
            {
                case GameStateType.Loss:
                    SoundType type = UnityEngine.Random.value > 0.5 ? SoundType.Willhelm : SoundType.TomScream;
                    player.PlayEffect(type);
                    player.PlayEffect(SoundType.SadTrombone);
                    break;
            }
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
            SoundType type = UnityEngine.Random.value > 0.5 ? SoundType.OMG : SoundType.Huh;
            player.PlayEffect(type);
        }

        private void SubscribeToEvents()
        {
            eventManager.Subscribe<OnCountdownTick>(this);
            eventManager.Subscribe<OnLetterChecked>(this);
            eventManager.Subscribe<OnMissileStateEnter>(this);
            eventManager.Subscribe<OnWordCompleted>(this);
            screenObjectOnTargetPositionInformer.OnReachingTargetPosition += OnScreenObjectShown;
            _stateController.OnStateChanged += OnStateChanged;
        }
    }
}
