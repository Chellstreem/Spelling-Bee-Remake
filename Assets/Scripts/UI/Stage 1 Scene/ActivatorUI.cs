using UnityEngine;
using Zenject;

public class ActivatorUI : IEventSubscriber<OnCountdownStateEnter>, 
    IEventSubscriber<OnCountdownStateExit>, IEventSubscriber<OnMissileStateEnter>,
    IEventSubscriber<OnMissileStateExit>
{
    private readonly EventManager eventManager;          
    private readonly GameObject countdownBar;       
    private readonly GameObject missileAlertBar;       

    public ActivatorUI(EventManager eventManager,
        [Inject(Id = UiObjectType.CountDownBar)] GameObject countdownBar,
        [Inject(Id = UiObjectType.MissileAlertBar)] GameObject missileAlertBar)
    {
        this.eventManager = eventManager;
        this.countdownBar = countdownBar;
        this.missileAlertBar = missileAlertBar; 
        
        SubscribeToEvents();
    }    
    
    public void OnEvent(OnCountdownStateEnter eventData) => ToggleCountdownBarActivation(true);    
    
    public void OnEvent(OnCountdownStateExit eventData) => ToggleCountdownBarActivation(false);

    public void OnEvent(OnMissileStateEnter eventData) => ToggleMissileAlertBarActivation(true);

    public void OnEvent(OnMissileStateExit eventData) => ToggleMissileAlertBarActivation(false);    

    private void ToggleCountdownBarActivation(bool isActivated) => countdownBar.SetActive(isActivated);    
    
    private void ToggleMissileAlertBarActivation(bool isActivated) => missileAlertBar.SetActive(isActivated);

    private void SubscribeToEvents()
    {
        eventManager.Subscribe<OnCountdownStateEnter>(this);
        eventManager.Subscribe<OnCountdownStateExit>(this);
        eventManager.Subscribe<OnMissileStateEnter>(this);
        eventManager.Subscribe<OnMissileStateExit>(this);
    }

    private void UnsubscribeFromEvents()
    {        
        eventManager.Unsubscribe<OnCountdownStateEnter>(this);
        eventManager.Unsubscribe<OnCountdownStateExit>(this);
        eventManager.Unsubscribe<OnMissileStateEnter>(this);
        eventManager.Unsubscribe<OnMissileStateExit>(this);
    }
}


