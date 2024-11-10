using UnityEngine;

[System.Serializable]
public class LifeManager
{
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public LifeManager(int initialMaxHealth)
    {
        maxHealth = initialMaxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void IncreaseMaxHealth(float lifeToIncrese)
    {
        maxHealth = maxHealth + lifeToIncrese;
        currentHealth = currentHealth + lifeToIncrese;
    }
}
