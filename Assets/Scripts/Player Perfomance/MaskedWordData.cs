using System.Collections.Generic;

public class MaskedWordData
{
    public string MaskedWord { get; set; }
    public Dictionary<string, int> HiddenIndices { get; set; }

    public MaskedWordData(string maskedWord, Dictionary<string, int> hiddenIndices)
    {
        MaskedWord = maskedWord;
        HiddenIndices = hiddenIndices;
    }    
}
