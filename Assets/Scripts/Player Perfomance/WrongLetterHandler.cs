namespace PlayerPerfomance
{
    public class WrongLetterHandler : IEventSubscriber<OnLetterChecked>
    {
        private readonly EventManager eventManager;
        private readonly IDamageDealer damageDealer;
        private readonly int damageOnWrongLetter;

        public WrongLetterHandler(EventManager eventManager, IDamageDealer damageDealer, PlayerPerfomanceConfig config)
        {
            this.eventManager = eventManager;
            this.damageDealer = damageDealer;
            damageOnWrongLetter = config.WrongLetterDamage;

            this.eventManager.Subscribe<OnLetterChecked>(this);
        }

        public void OnEvent(OnLetterChecked eventData)
        {
            if (!eventData.IsCorrect)
            {
                damageDealer.DamagePlayer(damageOnWrongLetter);
            }
        }
    }
}
