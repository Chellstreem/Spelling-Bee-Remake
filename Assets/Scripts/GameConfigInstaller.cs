using Zenject;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config Installer", menuName = "Installers/Game Config Installer")]
public class GameConfigInstaller : ScriptableObjectInstaller
{
    [SerializeField] private GameConfig _gameConfig;

    public override void InstallBindings()
    {
        Container.Bind<GameConfig>()
            .FromInstance(_gameConfig)
            .AsSingle()
            .NonLazy();
    }
}