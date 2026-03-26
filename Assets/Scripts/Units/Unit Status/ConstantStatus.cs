using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "Particle Config", menuName = "Scriptable Objects/Unit Status/Constant")]
    public class ConstantStatus : UnitStatusDefinition
    {
        public override void Enter(InteractableUnit unit)
        {
            unit.PlayEffect(_statusParticle);
        }

        public override void Exit(InteractableUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public override void HandleCollision(InteractableUnit unit, InteractableUnit other)
        {
            throw new System.NotImplementedException();
        }
    }
}