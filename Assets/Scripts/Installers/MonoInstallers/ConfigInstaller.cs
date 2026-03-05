using UnityEngine;
using Zenject;

public class ConfigInstaller : MonoInstaller
{    
    [SerializeField] private GameConfig gameConfig;          

    public override void InstallBindings()
    {        
        Container.Bind<GameConfig>().FromInstance(gameConfig).AsSingle();                      
    }
}
