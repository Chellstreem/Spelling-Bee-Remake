using System;
using System.Collections;
using TMPro;
using UnityEngine;
using WordControl;
using Zenject;

public class MaskedWordBar : MonoBehaviour, IEventSubscriber<OnLetterChecked>
{
    private WordController _wordController;

    private readonly string victoryMessage = "YOU DID IT!";
    private readonly float delayBeforeVictoryMessage = 1f;

    private TextMeshProUGUI text;

    [Inject]
    public void Construct(WordController wordController)
    {
        this._wordController = wordController;
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        UpdateText();
        SubscribeToEvents();
    }

    public void OnEvent()
    {
        StartCoroutine(UpdateVictoryMessageWithDelay(delayBeforeVictoryMessage));
    }

    public void OnEvent(OnLetterChecked eventData)
    {
        if (eventData.IsCorrect)
            UpdateText();
    }

    private void UpdateText()
    {
        text.text = _wordController.MaskedWord.CurrentMaskedWord.ToUpper();
    }

    private void SubscribeToEvents()
    {
        //eventManager.Subscribe<OnLetterChecked>(this);
        //eventManager.Subscribe<OnVictoryStateEnter>(this);
    }

    private void UnsubscribeFromEvents()
    {
        //eventManager.Unsubscribe<OnLetterChecked>(this);
        // eventManager.Unsubscribe<OnVictoryStateEnter>(this);
    }

    private IEnumerator UpdateVictoryMessageWithDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        text.text = victoryMessage;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}