using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;
using SceneControl;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UserInterface
{
    public class MainMenuBar : MonoBehaviour
    {
        [SerializeField] private int _activeFieldsOnStart = 1;
        [Inject] private SceneController _sceneController;

        [Header("Buttons")]
        [SerializeField] private Button _addWordButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        [Header("Input Fields")]
        [SerializeField] private InputField[] _inputFields;
        private int _activeFieldIndex;

        private void Start()
        {
            for (int i = 0; i < _inputFields.Length; i++)
                _inputFields[i].gameObject.SetActive(i < _activeFieldsOnStart);

            _activeFieldIndex = _activeFieldsOnStart - 1;

            AddListeners();
            UpdateButtonStates();
        }

        private void StartGame()
        {
            var words = CollectWords();

            if (words.Count == 0)
                return;

            StaticData.SavedWords = words;
            _sceneController.SwitchScene(SceneType.MainStage);
        }

        private List<string> CollectWords()
        {
            var words = new List<string>();

            foreach (var field in _inputFields)
            {
                if (!field.gameObject.activeSelf)
                    continue;

                var word = field.Input.text.Trim();
                if (!string.IsNullOrEmpty(word))
                    words.Add(word);

            }

            return words;
        }

        private void ActivateNextField()
        {
            if (_activeFieldIndex < _inputFields.Length - 1)
            {
                _activeFieldIndex++;
                _inputFields[_activeFieldIndex].Activate();

                UpdateButtonStates();
            }
        }

        private void DeactivateLastField()
        {
            if (_activeFieldIndex > 0)
            {
                _inputFields[_activeFieldIndex].Deactivate();
                _activeFieldIndex--;

                UpdateButtonStates();
            }
        }

        private void UpdateButtonStates()
        {
            _addWordButton.interactable = _activeFieldIndex < _inputFields.Length - 1;
            _deleteButton.interactable = _activeFieldIndex > 0;
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        private void AddListeners()
        {
            _addWordButton.onClick.AddListener(ActivateNextField);
            _deleteButton.onClick.AddListener(DeactivateLastField);
            _startButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(QuitGame);
        }
    }
}

