using SceneControl;
using UnityEngine;

public class GameStarter
{
    private readonly SceneSwitcher sceneSwitcher;
    private readonly InputFieldHandler inputFieldHandler;

    public GameStarter(SceneSwitcher sceneSwitcher, InputFieldHandler inputFieldHandler)
    {
        this.sceneSwitcher = sceneSwitcher;
        this.inputFieldHandler = inputFieldHandler;
    }

    public void StartGame()
    {
        var words = inputFieldHandler.GetWords();

        if (words.Count == 0)
        {
            Debug.LogWarning("Необходимо ввести хотя бы одно слово!");
            return;
        }

        StaticData.SavedWords = words;
        sceneSwitcher.SwitchScene(SceneName.Stage1);
    }
}
