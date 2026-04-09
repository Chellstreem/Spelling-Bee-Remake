using System;
using System.Collections.Generic;
using System.Text;

namespace WordControl
{
    public class WordController
    {
        private readonly WordControlConfig config;
        private string _availableSymbols;

        public static List<string> Words { get; set; }
        public string CurrentWord => Words[CurrentWordIndex];
        public int CurrentWordIndex { get; private set; }
        public MaskedWord MaskedWord { get; private set; }

        public event Action OnLetterRevealed;
        public event Action OnWordCompleted;
        public event Action OnAllWordsComleted;

        public WordController(WordControlConfig config)
        {
            this.config = config;
            Words = config.DefaultWords;
        }

        public void StartGame()
        {
            CurrentWordIndex = 0;

            MaskCurrentWord();
            GenerateAvailableSymbols();
        }

        public void MoveToNextWord()
        {
            CurrentWordIndex++;
            MaskCurrentWord();
            GenerateAvailableSymbols();
        }

        public bool IsCorrect(string value)
        {
            if (!MaskedWord.TryGetHiddenLetterIndex(value, out int hiddenIndex))
                return false;

            OnCorrectValue(value, hiddenIndex);
            return true;
        }

        public string GetRandomSymbol() => _availableSymbols[UnityEngine.Random.Range(0, _availableSymbols.Length)].ToString();
        public bool IsCurrentIndexLast() => CurrentWordIndex == Words.Count - 1;
        public int GetWordCount() => Words.Count;

        private void OnCorrectValue(string value, int index)
        {
            MaskedWord.RevealHiddenLetter(value, index);
            OnLetterRevealed?.Invoke();

            if (!MaskedWord.IsComplete)
                return;

            if (IsCurrentIndexLast())
            {
                OnAllWordsComleted?.Invoke();
                return;
            }

            MoveToNextWord();
            OnWordCompleted?.Invoke();
        }

        private void MaskCurrentWord()
        {
            int lettersToMask = config.GetLettersToMask(CurrentWord.Length);

            char[] maskedWordArray = CurrentWord.ToCharArray();
            Dictionary<string, int> maskedIndexes = new();

            while (maskedIndexes.Count < lettersToMask)
            {
                int randomIndex = UnityEngine.Random.Range(0, maskedWordArray.Length);
                string hiddenLetter = maskedWordArray[randomIndex].ToString();

                if (!maskedIndexes.ContainsKey(hiddenLetter))
                    maskedIndexes.Add(hiddenLetter, randomIndex);
            }

            foreach (var kvp in maskedIndexes)
                maskedWordArray[kvp.Value] = '_';

            string maskedWord = new(maskedWordArray);
            MaskedWord = new MaskedWord(maskedWord, maskedIndexes);
        }

        private void GenerateAvailableSymbols()
        {
            int extraSymbolCount = config.ExtraSymbolCount;
            string baseWord = CurrentWord;

            if (extraSymbolCount == 0)
                _availableSymbols = baseWord;

            HashSet<char> usedLetters = new(baseWord);
            List<char> available = new();

            foreach (var symbol in config.ExtraSymbols)
            {
                if (!usedLetters.Contains(symbol))
                    available.Add(symbol);
            }

            if (available.Count < extraSymbolCount)
                throw new ArgumentException("Not enough unique symbols");

            StringBuilder stringBuilder = new(baseWord, baseWord.Length + extraSymbolCount);

            for (int i = 0; i < extraSymbolCount; i++)
            {
                int index = UnityEngine.Random.Range(0, available.Count);
                stringBuilder.Append(available[index]);
                available.RemoveAt(index);
            }

            _availableSymbols = stringBuilder.ToString();
        }
    }
}
