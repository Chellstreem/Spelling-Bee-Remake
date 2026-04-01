using System.Collections;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "Invisible Unit Status", menuName = "Scriptable Objects/Unit Statuses/Invisible Unit Status")]
    public class InivisibleStatusDefinition : UnitStatusDefinition
    {
        public override void Enter(UnitStatus status)
        {
            Player player = status.Unit as Player;
            player.SetVisible(false);

            base.Enter(status);
        }

        public override void Exit(UnitStatus status)
        {
            Player player = status.Unit as Player;
            player.SetVisible(true);

            base.Exit(status);
        }
    }
}