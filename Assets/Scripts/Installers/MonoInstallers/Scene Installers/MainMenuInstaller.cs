using Zenject;
using UnityEngine;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private InputFieldHandler inputFieldHandler;
    [SerializeField] private FieldActivator fieldActivator;
    [SerializeField] private int initiallyActiveFields;

    public override void InstallBindings()
    {
        Container.Bind<InputFieldHandler>()
            .FromInstance(inputFieldHandler)
            .AsSingle();
        
        Container.Bind<FieldActivator>()
            .FromInstance(fieldActivator)
            .AsSingle();

        Container.Bind<int>()
            .FromInstance(initiallyActiveFields)
            .AsSingle();

        Container.Bind<GameStarter>().AsSingle();
    }
}
