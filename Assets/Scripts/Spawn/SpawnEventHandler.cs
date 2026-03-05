using System;
using Zenject;

namespace Spawn
{
    public class SpawnEventHandler :
        IEventSubscriber<OnMovingStateEnter>, IEventSubscriber<OnMovingStateExit>,
        IEventSubscriber<OnInteractiveSubstateEnter>, IEventSubscriber<OnInteractiveSubstateExit>,
        IEventSubscriber<OnMissileStateEnter>, IEventSubscriber<OnMissileStateExit>, IDisposable
    {
        private readonly EventManager eventManager;        
        private readonly ISpawner decorativeSpawner;
        private readonly ISpawner interactableSpawner;
        private readonly ISpawner missileSpawner;
        private readonly ISpawner monkeySpawner;

        public SpawnEventHandler(
            EventManager eventManager,            
            [Inject(Id = SpawnerType.Decorative)] ISpawner decorativeSpawner,
            [Inject(Id = SpawnerType.Interactable)] ISpawner interactableSpawner,
            [Inject(Id = SpawnerType.Missile)] ISpawner missileSpawner,
            [Inject(Id = SpawnerType.Monkey)] ISpawner monkeySpawner)
        {
            this.eventManager = eventManager;            
            this.decorativeSpawner = decorativeSpawner;
            this.interactableSpawner = interactableSpawner;
            this.missileSpawner = missileSpawner;
            this.monkeySpawner = monkeySpawner;
            
            SubscribeToEvents();            
        }

        public void OnEvent(OnMovingStateEnter eventData)
        {
            decorativeSpawner.StartSpawning();  
            interactableSpawner.StartSpawning();
        }

        public void OnEvent(OnMovingStateExit eventData)
        {
            decorativeSpawner.StopSpawning();
            interactableSpawner.StopSpawning();
        }

        public void OnEvent(OnInteractiveSubstateEnter eventData)
        {
            interactableSpawner.StartSpawning();
            monkeySpawner.StartSpawning();
        }           

        public void OnEvent(OnInteractiveSubstateExit eventData)
        {
            interactableSpawner.StopSpawning();
            monkeySpawner.StopSpawning();
        }

        public void OnEvent(OnMissileStateEnter eventData) => missileSpawner.StartSpawning();

        public void OnEvent(OnMissileStateExit eventData) => missileSpawner.StopSpawning();
        
        public void Dispose() => UnsubscribeFromEvents();        

        private void SubscribeToEvents()
        {
            eventManager.Subscribe<OnMovingStateEnter>(this);
            eventManager.Subscribe<OnMovingStateExit>(this);
            eventManager.Subscribe<OnInteractiveSubstateEnter>(this);
            eventManager.Subscribe<OnInteractiveSubstateExit>(this);
            eventManager.Subscribe<OnMissileStateEnter>(this);
            eventManager.Subscribe<OnMissileStateExit>(this);            
        }

        private void UnsubscribeFromEvents()
        {
            eventManager.Unsubscribe<OnMovingStateEnter>(this);
            eventManager.Unsubscribe<OnMovingStateExit>(this);
            eventManager.Unsubscribe<OnInteractiveSubstateEnter>(this);
            eventManager.Unsubscribe<OnInteractiveSubstateExit>(this);
            eventManager.Unsubscribe<OnMissileStateEnter>(this);
            eventManager.Unsubscribe<OnMissileStateExit>(this);            
        }
    }
}
