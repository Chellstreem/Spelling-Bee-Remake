using System.Collections;
using SceneControl;
using UnityEngine;


public class IntroSceneHandler
{
    private readonly CoroutineRunner coroutineRunner;
    private readonly SceneSwitcher sceneSwitcher;
    private readonly float sceneDuration;

    public IntroSceneHandler(CoroutineRunner coroutineRunner, SceneSwitcher sceneSwitcher, float sceneDuration)
    {
        this.coroutineRunner = coroutineRunner;
        this.sceneSwitcher = sceneSwitcher;
        this.sceneDuration = sceneDuration;

        this.coroutineRunner.StartCor(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        yield return new WaitForSeconds(sceneDuration);        
        sceneSwitcher.SwitchScene(SceneName.MainMenu);
    }
}




