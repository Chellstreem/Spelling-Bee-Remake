using UnityEngine.UI;
using UnityEngine;
using Zenject;
using SceneControl;

namespace UserInterface
{
    public class GameOverBar : MainStageBar
    {
        [Tooltip("Button that restarts the current gameplay scene when clicked")]
        [SerializeField] private Button _playAgainButton;
        [Tooltip("Button that returns the player to the main menu when clicked")]
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
