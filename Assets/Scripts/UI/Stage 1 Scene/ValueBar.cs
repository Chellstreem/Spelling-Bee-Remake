using TMPro;
using UnityEngine;

public class ValueBar : MonoBehaviour
{
    private ILetter letter;
    [SerializeField] private TextMeshProUGUI valueText;    

    private void Awake()
    {
        letter = GetComponent<ILetter>();              
    }

    private void OnEnable()
    {
        UpdateText(letter.Value);
        letter.OnLetterSet += UpdateText;
    }

    private void UpdateText(string value)
    {
        valueText.text = value.ToUpper();
    }

    private void OnDestroy() => letter.OnLetterSet -= UpdateText;   
}
