using Installers;
using UnityEngine;

namespace GameModules
{
    public abstract class GameModule : ScriptableObject
    {
        public abstract void Install(GameContext gameContext, MainStageInstaller installer, GameConfig config);
    }
}
