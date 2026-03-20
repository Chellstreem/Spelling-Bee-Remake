using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordControl
{
    public class WordController
    {
        private readonly WordControlConfig config;
        private List<string> _words;
        private string _availableSymbols;

        public string CurrentWord => _words[CurrentWordIndex];
        public int CurrentWordIndex { get; private set; }
        public MaskedWord MaskedWord { get; private set; }


        public event Action OnLetterRevealed;
        public event Action OnWordCompleted;
        public event Action OnAllWordsComleted;

        public WordController(WordControlConfig config) => this.config = config;

        public void StartGame()
        {
            _words = StaticData.SavedWords;
            CurrentWordIndex = 0;

            MaskCurrentWord();
            GenerateAvailableSymbols();
        }

        public void MoveToNextWord()
        {
            UnityEngine.Debug.Log("move");
            CurrentWordIndex++;
            MaskCurrentWord();
            GenerateAvailableSymbols();
        }

        public void OnValueChecked(string value)
        {
            if (!MaskedWord.TryGetHiddenLetterIndex(value, out int hiddenIndex))
                return;

            MaskedWord.RevealHiddenLetter(value, hiddenIndex);
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

        public string GetRandomSymbol() => _availableSymbols[UnityEngine.Random.Range(0, _availableSymbols.Length)].ToString();
        public bool IsCurrentIndexLast() => CurrentWordIndex == _words.Count - 1;
        public int GetWordCount() => _words.Count;

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
                {
                    maskedIndexes.Add(hiddenLetter, randomIndex);
                    maskedWordArray[randomIndex] = '_';
                }
            }

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
