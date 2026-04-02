using System.Collections;
using SceneControl;
using UnityEngine;


public class IntroSceneHandler
{
    private readonly CoroutineRunner coroutineRunner;
    private readonly SceneController sceneSwitcher;
    private readonly float sceneDuration;

    public IntroSceneHandler(CoroutineRunner coroutineRunner, SceneController sceneSwitcher, float sceneDuration)
    {
        this.coroutineRunner = coroutineRunner;
        this.sceneSwitcher = sceneSwitcher;
        this.sceneDuration = sceneDuration;

        this.coroutineRunner.StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(sceneDuration);
        sceneSwitcher.SwitchScene(SceneType.MainMenu);
    }
}




