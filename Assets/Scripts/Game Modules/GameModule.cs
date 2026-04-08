using Installers;
using UnityEngine;

namespace GameModules
{
    public abstract class GameModule : ScriptableObject
    {
        public abstract void Install(GameServices gameContext, SceneInstaller installer, GameConfig config);
    }
}
