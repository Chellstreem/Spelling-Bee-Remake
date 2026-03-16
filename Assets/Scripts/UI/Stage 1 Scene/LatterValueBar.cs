using Units;
using TMPro;
using UnityEngine;

public class LetterValueBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Letter _letter;

    private void OnEnable()
    {
        UpdateText();
        _letter.OnLetterUpdated += UpdateText;
    }

    private void UpdateText() => _valueText.text = _letter.Value.ToUpper();
    private void OnDisable() => _letter.OnLetterUpdated -= UpdateText;
    private void OnDestroy() => _letter.OnLetterUpdated -= UpdateText;
}
