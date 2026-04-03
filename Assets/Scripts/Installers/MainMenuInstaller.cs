using Zenject;
using VFX;
using SceneControl;
using UnityEngine;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private SceneCollection _sceneCollection;

    public override void InstallBindings()
    {
        ObjectScaler scaler = new();
        VisualEffectServices _visualServices = new();

        _visualServices.RegisterService(scaler);

        Container.Bind<VisualEffectServices>()
            .FromInstance(_visualServices)
            .AsSingle()
            .NonLazy();

        SceneController controller = new(_sceneCollection);

        Container.Bind<SceneController>()
            .FromInstance(controller)
            .AsSingle()
            .NonLazy();
    }
}
