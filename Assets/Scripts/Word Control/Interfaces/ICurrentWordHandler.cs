public interface ICurrentWordHandler
{
    public string CurrentWord { get; }

    public void MoveToNextWord();

    public bool IsCurrentIndexLast();
}
