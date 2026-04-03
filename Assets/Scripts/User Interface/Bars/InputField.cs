using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InputField : SimpleBar
    {
        [SerializeField] private TMP_InputField _inputField;

        public TMP_InputField Input => _inputField;

        private void Reset()
        {
            if (_inputField != null)
                return;

            _inputField = GetComponent<TMP_InputField>();

            if (_inputField == null)
                _inputField = GetComponentInChildren<TMP_InputField>(true);
        }
    }
}