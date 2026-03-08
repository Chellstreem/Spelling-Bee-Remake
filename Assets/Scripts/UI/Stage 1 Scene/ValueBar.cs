using InteractableObjects;
using TMPro;
using UnityEngine;

public class ValueBar : MonoBehaviour
{
    private Letter letter;
    [SerializeField] private TextMeshProUGUI valueText;

    private void Awake()
    {
        letter = GetComponent<Letter>();
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
