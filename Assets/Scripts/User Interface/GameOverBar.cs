using UnityEngine.UI;
using UnityEngine;
using Zenject;
using SceneControl;

namespace UserInterface
{
    public class GameOverBar : MainStageBar
    {
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _mainMenuButton;
        [Inject] private SceneController _sceneController;

        private void OnEnable()
        {
            _playAgainButton.onClick.AddListener(PlayAgain);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
        }

        private void PlayAgain() => _sceneController.LoadScene(SceneType.MainStage);
        private void GoToMainMenu() => _sceneController.LoadScene(SceneType.MainMenu);

        private void OnDisable()
        {
            _playAgainButton.onClick.RemoveListener(PlayAgain);
            _mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        }
    }
}
