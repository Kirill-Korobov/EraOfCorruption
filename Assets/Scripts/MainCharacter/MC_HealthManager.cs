using System.Collections;
using TMPro;
using UnityEngine;

public class MC_HealthManager : MonoBehaviour
{
    private int defense;
    [SerializeField] private BloodyBackgroundBehaviour bloodyBackgroundBehaviour;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_ManaManager manaManager;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private MainGameUIOperator mainGameUIOperator;
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private TMP_Text remainingRespawnTimeText, moneyLossText;
    private Coroutine waitUntilHealthCanBeReplenished, dieCoroutine;
    private GameStats currentGameStats;

    private void Start()
    {
        deathCanvas.gameObject.SetActive(false);
        switch (GameStatsManager.currentGame)
        {
            case 1:
                currentGameStats = gameStatsManager.game1Stats;
                break;
            case 2:
                currentGameStats = gameStatsManager.game2Stats;
                break;
            case 3:
                currentGameStats = gameStatsManager.game3Stats;
                break;
            default:
                currentGameStats = gameStatsManager.game1Stats;
                break;
        }
        Health = currentGameStats.mainCharacterStats.health;
        RemainingRespawnTime = currentGameStats.mainCharacterStats.remainingRespawnTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public float Health
    {
        get
        {
            return currentGameStats.mainCharacterStats.health;
        }
        set
        {
            if (value <= 0)
            {
                currentGameStats.mainCharacterStats.health = 0;
                if (dieCoroutine == null)
                {
                    dieCoroutine = StartCoroutine(DieCoroutine());
                }
            }
            else if (value > statisticsInfo.MaxHPValues[statisticsManager.HPLevel])
            {
                currentGameStats.mainCharacterStats.health = statisticsInfo.MaxHPValues[statisticsManager.HPLevel];
            }
            else
            {
                currentGameStats.mainCharacterStats.health = value;
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

    public float RemainingRespawnTime
    {
        get
        {
            return currentGameStats.mainCharacterStats.remainingRespawnTime;
        }
        set
        {
            if (value <= 0)
            {
                currentGameStats.mainCharacterStats.remainingRespawnTime = 0;
            }
            else
            {
                currentGameStats.mainCharacterStats.remainingRespawnTime = value;
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

    public IEnumerator WaitUntilHealthCanBeReplenished()
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

    private IEnumerator DieCoroutine()
    {
        pauseManager.SetGamePaused();
        mainGameUIOperator.mainCanvas.gameObject.SetActive(false);
        // Turn off all effects.
        // Minus money.
        // Destroy all enemies and bosses.
        deathCanvas.gameObject.SetActive(true);
        // Enter loss money value.
        moneyLossText.text = $"Loss of money: {0}";
        if (RemainingRespawnTime <= 0)
        {
            RemainingRespawnTime = statisticsInfo.RespawnTime;
        }

        while (RemainingRespawnTime > 0)
        {
            RemainingRespawnTime -= Time.unscaledDeltaTime;
            remainingRespawnTimeText.text = $"You`ll respawn in {Mathf.CeilToInt(RemainingRespawnTime).ToString("f0")} seconds";
            yield return null;
        }

        deathCanvas.gameObject.SetActive(false);
        mainGameUIOperator.mainCanvas.gameObject.SetActive(true);
        bloodyBackgroundBehaviour.bloodyBackgroundImage.color = new Color(bloodyBackgroundBehaviour.bloodyBackgroundImage.color.r, bloodyBackgroundBehaviour.bloodyBackgroundImage.color.g, bloodyBackgroundBehaviour.bloodyBackgroundImage.color.b, 0f);
        pauseManager.SetGameNotPaused();
        Health = statisticsInfo.MaxHPValues[statisticsManager.HPLevel] / 2;
        energyManager.Energy = statisticsInfo.MaxEnergyValues[statisticsManager.EnergyLevel];
        manaManager.Mana = statisticsInfo.ÑloseCombatAdditionalManaValues[statisticsManager.CloseCombatLevel] + statisticsInfo.RangedCombatAdditionalManaValues[statisticsManager.RangedCombatLevel] + statisticsInfo.MagicCombatAdditionalManaValues[statisticsManager.MagicCombatLevel];
        satietyManager.Satiety = statisticsInfo.SatietyMaxValue;
        // Set main character`s position.
        dieCoroutine = null;
    }
}