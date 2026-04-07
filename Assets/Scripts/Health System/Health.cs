using System;
using UnityEngine;

namespace HealthSystem
{
    public class Health
    {
        private readonly int maxHealth;
        private readonly int startHealth;

        public int CurrentHealth { get; private set; }
        public float HealthRatio => (float)CurrentHealth / maxHealth;

        public event Action OnHealthChanged;

        public Health(int maxHealth, int startHealth)
        {
            this.maxHealth = maxHealth;
            this.startHealth = startHealth;
        }

        public void Refresh()
        {
            CurrentHealth = Math.Min(startHealth, maxHealth);
            OnHealthChanged?.Invoke();
        }

        public void Damage(int amount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            OnHealthChanged?.Invoke();
        }

        public void Restore(int amount)
        {
            CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
            OnHealthChanged?.Invoke();
        }
    }
}
