using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InputField : SimpleBar
    {
        [Tooltip("Reference to the TMP_InputField component used for text input")]
        [SerializeField] private TMP_InputField _inputField;

        public TMP_InputField Input => _inputField;
    }
}