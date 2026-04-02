using UnityEngine;
using TMPro;
using Zenject;

public class IntroSceneInstaller : MonoInstaller
{
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

        Container.Bind<IntroSceneHandler>().AsSingle().NonLazy();
        Container.Bind<TitleController>().AsSingle().NonLazy();
    }
}
