using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "InputInstaller", menuName = "Installers/InputInstaller")]
public class InputInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    { 

#if UNITY_ANDROID
        Container.BindInterfacesTo<InputSystem.TouchInput>().AsSingle().NonLazy();        
#else
        Container.BindInterfacesTo<InputSystem.DesktopInput>().AsSingle().NonLazy();        
#endif
    }
}
