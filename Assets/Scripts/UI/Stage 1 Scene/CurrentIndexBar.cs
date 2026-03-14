using TMPro;
using UnityEngine;
using Zenject;
using WordControl;

public class CurrentIndexBar : MonoBehaviour, IEventSubscriber<OnWordCompleted>
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

    }

    public void OnEvent(OnWordCompleted eventData)
    {
        UpdateIndex();
    }


    private void UpdateIndex()
    {
        text.text = $"{indexGetter.CurrentWordIndex} / {wordCount}";
    }

    private void OnDestroy()
    {
        eventManager.Unsubscribe<OnWordCompleted>(this);

    }
}
