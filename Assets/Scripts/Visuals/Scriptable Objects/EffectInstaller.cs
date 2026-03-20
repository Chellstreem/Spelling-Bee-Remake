using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EffectInstaller", menuName = "Installers/EffectInstaller")]
public class EffectInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IScaler>().To<ScaleEffect>().AsSingle();
    }
}
