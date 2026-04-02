using SceneControl;
using UnityEngine;

public class GameStarter
{
    private readonly SceneController sceneSwitcher;
    private readonly InputFieldHandler inputFieldHandler;

    public GameStarter(SceneController sceneSwitcher, InputFieldHandler inputFieldHandler)
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
        sceneSwitcher.SwitchScene(SceneType.MainStage);
    }
}
