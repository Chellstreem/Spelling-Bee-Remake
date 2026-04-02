using UnityEngine.UI;
using UnityEngine;
using Zenject;
using SceneControl;

namespace UserInterface
{
    public class GameOverBar : UIBar
    {
        private SceneController sceneSwitcher;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainMenuButton;

        [Inject]
        public void Construct(SceneController sceneSwitcher)
        {
            this.sceneSwitcher = sceneSwitcher;
        }

        private void OnEnable()
        {
            playAgainButton.onClick.AddListener(PlayAgain);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }

        private void PlayAgain() => sceneSwitcher.SwitchScene(SceneType.MainStage);

        private void GoToMainMenu() => sceneSwitcher.SwitchScene(SceneType.MainMenu);

        private void OnDisable()
        {
            playAgainButton.onClick.RemoveListener(PlayAgain);
            mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        }
    }
}
