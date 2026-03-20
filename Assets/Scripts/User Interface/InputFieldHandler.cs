using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InputFieldHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] inputFields;

    public List<string> GetWords()
    {
        var words = new List<string>();

        foreach (var input in inputFields)
        {
            if (input.gameObject.activeSelf)
            {
                var word = input.text.Trim();
                if (!string.IsNullOrEmpty(word))
                    words.Add(word);
            }
        }

        return words;
    }
}
