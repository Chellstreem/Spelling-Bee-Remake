using System.Collections.Generic;

namespace WordControl
{
    public class MaskedWord
    {
        public readonly string maskedWord;
        public readonly Dictionary<string, int> hiddenIndices;

        public string CurrentMaskedWord { get; private set; }

        public MaskedWord(string maskedWord, Dictionary<string, int> hiddenIndices)
        {
            this.maskedWord = maskedWord;
            this.hiddenIndices = hiddenIndices;
            CurrentMaskedWord = this.maskedWord;
        }

        public bool TryGetHiddenLetterIndex(string letter, out int hiddenIndex)
        {
            if (hiddenIndices.TryGetValue(letter, out int index))
            {
                hiddenIndex = index;
                return true;
            }

            hiddenIndex = -1;
            return false;
        }

        public void RevealHiddenLetter(string letter, int index)
        {
            char[] maskedWordArray = CurrentMaskedWord.ToCharArray();
            maskedWordArray[index] = letter[0];
            CurrentMaskedWord = new string(maskedWordArray);

            hiddenIndices.Remove(letter);
        }
    }
}
