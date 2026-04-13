using System.Collections.Generic;
using UnityEngine;

namespace WordControl
{
    [CreateAssetMenu(fileName = "Word Control Config", menuName = "Scriptable Objects/Word Control Config")]
    public class WordControlConfig : ScriptableObject
    {
        [Tooltip("The number of additional symbol added to the letters of the current word in order to increase difficulty")]
        [SerializeField] private int _extraSymbolCount = 5;
        [Tooltip("Characters used to fill extra symbol slots when generating distractor letters")]
        [SerializeField] private string _availableExtraSymbols = "_abcdefghijklmnopqrstuvwxyz";
        [Tooltip("Word length threshold above which more letters are masked")]
        [SerializeField] private int _maskedLetterThreshold = 5;
        [Tooltip("Minimum number of letters to mask when masking is applied")]
        [SerializeField] private int _maskedLetterMinNum = 1;
        [Tooltip("Maximum number of letters to mask when masking is applied")]
        [SerializeField] private int _maskedLetterMaxNum = 2;
        [Tooltip("Default list of words used when no custom list is provided")]
        [SerializeField] private List<string> _defaultWords;

        public int ExtraSymbolCount => _extraSymbolCount;
        public string ExtraSymbols => _availableExtraSymbols;
        public List<string> DefaultWords => _defaultWords;

        public int GetLettersToMask(int wordLength)
        {
            int lettersToMask = wordLength > _maskedLetterThreshold ? _maskedLetterMaxNum : _maskedLetterMinNum;
            return lettersToMask;
        }
    }
}
