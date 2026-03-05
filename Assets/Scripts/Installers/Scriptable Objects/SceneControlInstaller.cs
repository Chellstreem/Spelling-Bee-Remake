using SceneControl;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Scene Control Installer", menuName = "Installers/Scene Control Installer")]
public class SceneControlInstaller : ScriptableObjectInstaller
{
    [SerializeField] private SceneCollection sceneCollection;

    public override void InstallBindings()
    {
        Container.Bind<SceneCollection>()
            .FromInstance(sceneCollection)
            .AsSingle();

        Container.Bind<SceneSwitcher>().AsSingle();
    }
}
