using GameStates;
using Zenject;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateInstaller", menuName = "Installers/GameStateInstaller")]
public class GameStateInstaller : ScriptableObjectInstaller
{
    [SerializeField] private GameStateConfig _config;

    public override void InstallBindings()
    {
        var controller = new GameStateController(_config);

        Container.Bind<GameStateController>()
            .FromInstance(controller)
            .AsSingle();
    }
}
