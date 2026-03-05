using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class CountdownText : MonoBehaviour, IEventSubscriber<OnCountdownTick>
{
    private EventManager eventManager;
    private IScaler scaler;
    private TextMeshProUGUI text;        

    [Inject]
    public void Construct(EventManager eventManager, IScaler scaler)
    {        
        this.eventManager = eventManager;
        this.scaler = scaler;        
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        eventManager.Subscribe<OnCountdownTick>(this);
    }

    public void OnEvent(OnCountdownTick eventData)
    {       
        int count  = eventData.Count;
        int fontSize = eventData.FontSize;
        int finalFontSize = eventData.FinalFontSize;
        UpdateCountdown(count, fontSize, finalFontSize);
        scaler.ActivateWithScale(text.transform, 0.7f, 0, Ease.InOutQuad);
    }

    public void UpdateCountdown(int count, int fontSize, int goFontSize)
    {        
        if (count == 0)
        {

            text.fontSize = goFontSize;
            text.text = "GO!";            
        }
        else
        {
            text.fontSize = fontSize;
            text.text = count.ToString();            
        }
    }

    private void OnDestroy() => eventManager.Unsubscribe<OnCountdownTick>(this);    
}
