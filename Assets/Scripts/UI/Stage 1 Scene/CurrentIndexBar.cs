using TMPro;
using UnityEngine;
using Zenject;
using WordControl;

public class CurrentIndexBar : MonoBehaviour, IEventSubscriber<OnWordCompleted>, IEventSubscriber<OnVictoryStateEnter>
{
    private EventManager eventManager;
    private WordController indexGetter;

    private TextMeshProUGUI text;
    private int wordCount;

    [Inject]
    public void Construct(EventManager eventManager, WordController indexGetter)
    {
        this.eventManager = eventManager;
        this.indexGetter = indexGetter;
    }

    private void Awake()
    {
        wordCount = indexGetter.GetWordCount();

        text = GetComponent<TextMeshProUGUI>();
        UpdateIndex();

        eventManager.Subscribe<OnWordCompleted>(this);
        eventManager.Subscribe<OnVictoryStateEnter>(this);
    }

    public void OnEvent(OnWordCompleted eventData)
    {
        UpdateIndex();
    }

    public void OnEvent(OnVictoryStateEnter eventData)
    {
        text.text = $"{wordCount} / {wordCount}";
    }

    private void UpdateIndex()
    {
        text.text = $"{indexGetter.CurrentWordIndex} / {wordCount}";
    }

    private void OnDestroy()
    {
        eventManager.Unsubscribe<OnWordCompleted>(this);
        eventManager.Unsubscribe<OnVictoryStateEnter>(this);
    }
}
