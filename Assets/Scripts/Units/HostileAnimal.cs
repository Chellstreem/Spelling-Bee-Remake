namespace Units
{
    public class HostileAnimal : ComplexUnit
    {
        public override ComplexUnitType ComplexUnitType => ComplexUnitType.HostileAnimal;

        public void OnEnable() => IsInteractable = true;

        public override void HandleCollision(Unit other)
        {
            if (!IsInteractable || !other.IsInteractable)
                return;

            if (!StatusController.CurrentStatus.Definition.CanDealDamage)
                return;

            InvokeAttack();
            other.Damage(_damage);
        }

        public override void Damage(int damage)
        {
            if (!StatusController.CurrentStatus.Definition.CanTakeDamage)
                return;

            InvokeDeath();
        }

        public override void InvokeUnitSound()
        {
            if (IsInteractable)
                return;

            base.InvokeUnitSound();
        }
    }
}
