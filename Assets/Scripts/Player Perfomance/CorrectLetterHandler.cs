using Zenject;

namespace PlayerPerfomance
{
    public class CorrectLetterHandler : IEventSubscriber<OnLetterChecked>
    {
        private readonly EventManager eventManager;
        private readonly ICurrentWordHandler currentWord;
        private readonly IMaskedWordHandler maskedWord;

        public CorrectLetterHandler(EventManager eventManager, ICurrentWordHandler currentWordHandler,
            IMaskedWordHandler maskedWordHandler)
        {
            this.eventManager = eventManager;
            currentWord = currentWordHandler;
            maskedWord = maskedWordHandler;

            this.eventManager.Subscribe<OnLetterChecked>(this);
        }

        [Inject]
        public void Initialize()
        {
            UpdateMaskedWord(currentWord.CurrentWord);
        }

        public void OnEvent(OnLetterChecked eventData)
        {
            if (!eventData.IsCorrect) return;

            maskedWord.RevealHiddenLetter(eventData.Letter);
            if (maskedWord.GetMaskedWord() != currentWord.CurrentWord) return;
            if (currentWord.IsCurrentIndexLast())
            {
                eventManager.Publish(new OnAllWordsCompleted());
            }
            else
            {
                currentWord.MoveToNextWord();
                UpdateMaskedWord(currentWord.CurrentWord);
                eventManager.Publish(new OnWordCompleted());
            }
        }

        private void UpdateMaskedWord(string word)
        {
            maskedWord.SetCurrentMaskedWord(word);
        }
    }
}
