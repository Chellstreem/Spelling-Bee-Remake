using UnityEngine;
using TMPro;
using Zenject;
using SceneControl;

public class IntroSceneInstaller : MonoInstaller
{
    [SerializeField] private SceneCollection _sceneCollection;
    [SerializeField] private GameObject coroutineRunnerPrefab;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private float sceneDuration;
    [SerializeField] private int finalFontSize;

    public override void InstallBindings()
    {
        Container.Bind<CoroutineRunner>()
            .FromComponentInNewPrefab(coroutineRunnerPrefab)
            .AsSingle();

        Container.Bind<TextMeshProUGUI>()
            .FromInstance(title)
            .AsSingle();

        Container.Bind<float>()
            .FromInstance(sceneDuration)
            .AsSingle();

        Container.Bind<int>()
            .FromInstance(finalFontSize)
            .AsSingle();

        SceneController sceneController = new(_sceneCollection);

        Container.Bind<SceneController>()
        .FromInstance(sceneController)
        .AsSingle()
        .NonLazy();

        Container.Bind<IntroSceneHandler>().AsSingle().NonLazy();
    }
}
