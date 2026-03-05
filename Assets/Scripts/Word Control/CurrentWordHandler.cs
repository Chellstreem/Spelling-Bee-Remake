using System;
using System.Collections.Generic;

public class CurrentWordHandler : ICurrentWordHandler, ICurrentIndexGetter, ICurrentWordGetter
{
    private readonly List<string> words;
    private int currentWordIndex;

    public string CurrentWord => words[currentWordIndex];
    public event Action OnNewCurrentWord;

    public CurrentWordHandler()
    {
        words = StaticData.SavedWords;
        currentWordIndex = 0;
    }    

    public int GetCurrentWordIndex() => currentWordIndex;

    public void MoveToNextWord()
    { 
        currentWordIndex++;
        OnNewCurrentWord?.Invoke();
    }

    public bool IsCurrentIndexLast() => currentWordIndex == words.Count - 1;   

    public int GetTotalWords() => words.Count;    
}
