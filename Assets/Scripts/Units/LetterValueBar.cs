using Units;
using TMPro;
using UnityEngine;

namespace Units
{
    public class LetterValueBar : MonoBehaviour
    {
        [Tooltip("Text element used to display the letter value")]
        [SerializeField] private TextMeshProUGUI _valueText;
        [Tooltip("Reference to the Letter component providing the value")]
        [SerializeField] private Letter _letter;

        private void OnEnable()
        {
            UpdateText();
            _letter.OnLetterUpdated += UpdateText;
        }

        private void UpdateText() => _valueText.text = _letter.Value.ToUpper();
        private void OnDisable() => _letter.OnLetterUpdated -= UpdateText;
    }
}
