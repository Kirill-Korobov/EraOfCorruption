using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MC_HealthManager : MonoBehaviour
{
    [SerializeField] private BloodyBackgroundBehaviour bloodyBackgroundBehaviour;
    [SerializeField] private MC_SatietyManager satietyManager;
    private Coroutine waitUntilHealthCanBeReplenished;
    private float currentHealth, maxHealth;
    private int defense;

    private void Awake()
    {
        MaxHealth = 100;
        CurrentHealth = 100;
        // Set health stats.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(20);
        }
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

    [HideInInspector] public float hex = 1;
    public void TakeDamage(float value)
    {
        float finalDamage;
        if (Defense >= value * hex)
        {
            finalDamage = 1;
        }
        else
        {
            finalDamage = value * hex - Defense;
        }
        CurrentHealth -= finalDamage;
        if (bloodyBackgroundBehaviour.bloodyBackgroundImage.color.a + finalDamage / maxHealth * bloodyBackgroundBehaviour.bloodMultiplier < bloodyBackgroundBehaviour.maxBloodyBackgroundOpacity)
        {
            bloodyBackgroundBehaviour.bloodyBackgroundImage.color += new Color(0f, 0f, 0f, finalDamage / maxHealth * bloodyBackgroundBehaviour.bloodMultiplier);
        }
        else
        {
            bloodyBackgroundBehaviour.bloodyBackgroundImage.color = new Color(1f, 0f, 0f, bloodyBackgroundBehaviour.maxBloodyBackgroundOpacity);
        }
        if (waitUntilHealthCanBeReplenished != null)
        {
            StopCoroutine(waitUntilHealthCanBeReplenished);
        }
        satietyManager.canReplenishHealth = false;
        waitUntilHealthCanBeReplenished = StartCoroutine(WaitUntilHealthCanBeReplenished());
    }

    private IEnumerator WaitUntilHealthCanBeReplenished()
    {
        yield return new WaitForSeconds(satietyManager.timeUntilHealthCanBeReplenished);
        Debug.Log("lole");
        satietyManager.canReplenishHealth = true;
    }

    [HideInInspector] public bool cursed = false;
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