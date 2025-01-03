using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MC_HealthManager : MonoBehaviour
{
    private float health;
    private int defense;
    [SerializeField] private BloodyBackgroundBehaviour bloodyBackgroundBehaviour;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    private Coroutine waitUntilHealthCanBeReplenished;

    private void Awake()
    {
        // Set health stats.
        Health = 100;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(20);
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value <= 0)
            {
                health = 0;
                Die();
            }
            else if (value > statisticsInfo.MaxHPValues[statisticsManager.HPLevel])
            {
                health = statisticsInfo.MaxHPValues[statisticsManager.HPLevel];
            }
            else
            {
                health = value;
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
    [HideInInspector] public float penetration = 1;
    public void TakeDamage(float value)
    {
        float finalDamage;
        if (Defense * penetration>= value * hex)
        {
            finalDamage = 1;
        }
        else
        {
            finalDamage = value * hex - Defense * penetration;
        }
        Health -= finalDamage;
        if (bloodyBackgroundBehaviour.bloodyBackgroundImage.color.a + finalDamage / statisticsInfo.MaxHPValues[statisticsManager.HPLevel] * bloodyBackgroundBehaviour.bloodMultiplier < bloodyBackgroundBehaviour.maxBloodyBackgroundOpacity)
        {
            bloodyBackgroundBehaviour.bloodyBackgroundImage.color += new Color(0f, 0f, 0f, finalDamage / statisticsInfo.MaxHPValues[statisticsManager.HPLevel] * bloodyBackgroundBehaviour.bloodMultiplier);
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
        yield return new WaitForSeconds(statisticsInfo.TimeUntilHealthCanBeReplenished);
        satietyManager.canReplenishHealth = true;
    }

    [HideInInspector] public bool cursed = false;
    public void GetHealth(float value)
    {
        if (!cursed)
        {
            Health += value;
        }
    }

    private void Die()
    {
        Debug.Log("You died.");
        // Respawn.
    }
}