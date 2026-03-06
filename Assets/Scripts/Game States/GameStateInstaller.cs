using GameStates;
using Zenject;
using UnityEngine;
using System.Threading.Tasks;

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

        foreach (var state in _config.GameStates)
            state.Initialize(controller, Container.Resolve<EventManager>(), Container.Resolve<CoroutineRunner>());


        controller.SetState(GameStateType.Countdown);
    }
}
