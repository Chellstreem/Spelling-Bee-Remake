using TMPro;
using UnityEngine;
using UserInterface;
using WordControl;
using Zenject;

public class CurrentWordBar : MainStageBar
{
    [Header("References")]
    [Tooltip("Text element that displays the currently active masked word")]
    [SerializeField] private TextMeshProUGUI _currentWordText;
    [Tooltip("Text element that displays the current word index / total count")]
    [SerializeField] private TextMeshProUGUI _currentIndexText;
    private WordController _wordController;
    private string _victoryMessage;

    [Inject]
    public void Construct(WordController wordController, GameConfig gameConfig)
    {
        _wordController = wordController;
        _victoryMessage = gameConfig.VictoryText;
    }

    private void Start()
    {
        UpdateText();
        _wordController.OnLetterRevealed += UpdateText;
        _wordController.OnWordCompleted += UpdateText;
        _wordController.OnAllWordsComleted += OnAllWordsCompleted;
    }

    private void OnEnable()
    {
        if (_wordController != null)
            UpdateText();
    }

    private void UpdateText()
    {
        _currentWordText.text = _wordController.MaskedWord.CurrentMaskedWord.ToUpper();

        int currentIndex = _wordController.CurrentWordIndex + 1;
        int length = _wordController.GetWordCount();
        _currentIndexText.text = $"{currentIndex} / {length}";
    }

    private void OnAllWordsCompleted() => _currentWordText.text = _victoryMessage;

    private void OnDestroy()
    {
        _wordController.OnLetterRevealed -= UpdateText;
        _wordController.OnWordCompleted -= UpdateText;
        _wordController.OnAllWordsComleted -= OnAllWordsCompleted;
    }
}