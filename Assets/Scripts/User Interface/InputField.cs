using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InputField : SimpleBar
    {
        [SerializeField] private TMP_InputField _inputField;

        public TMP_InputField Input => _inputField;
    }
}