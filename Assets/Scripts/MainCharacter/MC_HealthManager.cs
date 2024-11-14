using UnityEngine;

public class MC_HealthManager : MonoBehaviour
{
    private float currentHealth, maxHealth;
    private int defense;

    private void Awake()
    {
        // Set health stats.
    }

    private float Health
    {
        get 
        { 
            return currentHealth; 
        }
        set
        {
            if (value < 0)
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

    public void TakeDamage(float value)
    {
        if (defense >= value)
        {
            Health -= 1;
        }
        else
        {
            Health -= value;
        }
    }

    public void GetHealth(float value) 
    {
        Health += value;
    }

    private void Die()
    {
        Debug.Log("You died.");
        // Respawn.
    }
}