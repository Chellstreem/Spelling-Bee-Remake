using System;

public interface ICurrentWordGetter
{
    public string CurrentWord { get; }

    public event Action OnNewCurrentWord;
}
