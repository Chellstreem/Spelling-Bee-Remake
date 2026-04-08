using System.Collections.Generic;
using UnityEngine;

namespace WordControl
{
    [CreateAssetMenu(fileName = "Word Control Config", menuName = "Scriptable Objects/Word Control Config")]
    public class WordControlConfig : ScriptableObject
    {
        [Tooltip("The number of additional symbol added to the letters of the current word in order to increase difficulty")]
        [SerializeField] private int _extraSymbolCount = 5;
        [SerializeField] private string _availableExtraSymbols = "_abcdefghijklmnopqrstuvwxyz";
        [SerializeField] private int _maskedLetterThreshold = 5;
        [SerializeField] private int _maskedLetterMinNum = 1;
        [SerializeField] private int _maskedLetterMaxNum = 2;
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
