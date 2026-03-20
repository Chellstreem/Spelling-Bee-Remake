using UnityEngine.UI;
using UnityEngine;
using Zenject;
using SceneControl;

public class GameOverMenu : MonoBehaviour
{
    private SceneSwitcher sceneSwitcher;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    [Inject]
    public void Construct(SceneSwitcher sceneSwitcher)
    {
        this.sceneSwitcher = sceneSwitcher;
    }

    private void OnEnable()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void PlayAgain() => sceneSwitcher.SwitchScene(SceneName.Stage1);

    private void GoToMainMenu() => sceneSwitcher.SwitchScene(SceneName.MainMenu);

    private void OnDisable()
    {
        playAgainButton.onClick.RemoveListener(PlayAgain);
        mainMenuButton.onClick.RemoveListener(GoToMainMenu);       
    }
}
