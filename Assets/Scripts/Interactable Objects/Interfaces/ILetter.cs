using System;

public interface ILetter
{
    public string Value { get; }
    public event Action<string> OnLetterSet;
}
