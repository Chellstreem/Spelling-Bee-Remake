using System;
using UnityEngine;

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

    public void Refresh() => CurrentHealth = Math.Min(startHealth, maxHealth);

    public void Damage(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke();
    }

    public void RestorePlayerLives(int amount)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        OnHealthChanged?.Invoke();
    }
}
