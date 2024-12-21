using UnityEngine;

public class MC_HealthManager : MonoBehaviour
{
    private float currentHealth, maxHealth;
    private int defense;

    private void Awake()
    {
        MaxHealth = 100;
        CurrentHealth = 100;
        // Set health stats.
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else if (value > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            if (value > 0)
            {
                maxHealth = value;
            }       
        }
    }

    public int Defense
    {
        get
        {
            return defense;
        }
        set
        {
            if (value >= 0)
            {
                defense = value;
            }
        }
    }
    public float hex = 1;
    public void TakeDamage(float value)
    {
        if (Defense >= value * hex)
        {
            CurrentHealth -= hex;
        }
        else
        {
            CurrentHealth -= value*hex - Defense;
        }
    }

    public bool cursed = false;
    public void GetHealth(float value)
    {
        if (!cursed)
        {
            CurrentHealth += value;
        }
    }

    private void Die()
    {
        Debug.Log("You died.");
        // Respawn.
    }
}