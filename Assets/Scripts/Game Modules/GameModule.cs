using Installers;
using UnityEngine;
using Zenject;

namespace GameModules
{
    public abstract class GameModule : ScriptableObject
    {
        public abstract void Install(SceneInstaller installer, GameConfig config);

        public virtual void Dispose(SceneInstaller installer) { }
    }
}
