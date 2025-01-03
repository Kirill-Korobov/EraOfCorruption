using System;
using System.IO;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public static int currentGame;
    private string game1StatsSavePath, game2StatsSavePath, game3StatsSavePath;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [HideInInspector] public GameStats game1Stats, game2Stats, game3Stats;

    private void Awake()
    {
        game1StatsSavePath = $"{Application.persistentDataPath}/Game1Stats.json";
        game2StatsSavePath = $"{Application.persistentDataPath}/Game2Stats.json";
        game3StatsSavePath = $"{Application.persistentDataPath}/Game3Stats.json";

        game1Stats.statisticsInfo = statisticsInfo;
        game2Stats.statisticsInfo = statisticsInfo;
        game3Stats.statisticsInfo = statisticsInfo;

        if (File.Exists(game1StatsSavePath))
        {
            string json = string.Empty;
            using (var reader = new StreamReader(game1StatsSavePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) { json += line; }
            }
            game1Stats = JsonUtility.FromJson<GameStats>(json);
        }
        else
        {
            game1Stats.SetAllStatsToZero();
            using (var writer = new StreamWriter(game1StatsSavePath))
            {
                writer.WriteLine(JsonUtility.ToJson(game1Stats));
            }
        }

        if (File.Exists(game2StatsSavePath))
        {
            string json = string.Empty;
            using (var reader = new StreamReader(game2StatsSavePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) { json += line; }
            }
            game2Stats = JsonUtility.FromJson<GameStats>(json);
        }
        else
        {
            game2Stats.SetAllStatsToZero();
            using (var writer = new StreamWriter(game2StatsSavePath))
            {
                writer.WriteLine(JsonUtility.ToJson(game2Stats));
            }
        }

        if (File.Exists(game3StatsSavePath))
        {
            string json = string.Empty;
            using (var reader = new StreamReader(game3StatsSavePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) { json += line; }
            }
            game3Stats = JsonUtility.FromJson<GameStats>(json);
        }
        else
        {
            game3Stats.SetAllStatsToZero();
            using (var writer = new StreamWriter(game3StatsSavePath))
            {
                writer.WriteLine(JsonUtility.ToJson(game3Stats));
            }
        }
    }

    public void SaveStats()
    {
        using (var writer = new StreamWriter(game1StatsSavePath))
        {
            writer.WriteLine(JsonUtility.ToJson(game1Stats));
        }
        using (var writer = new StreamWriter(game2StatsSavePath))
        {
            writer.WriteLine(JsonUtility.ToJson(game2Stats));
        }
        using (var writer = new StreamWriter(game3StatsSavePath))
        {
            writer.WriteLine(JsonUtility.ToJson(game3Stats));
        }
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }
}

[Serializable]
public class GameStats
{
    public SlotStats slotStats;
    public MainCharacterStats mainCharacterStats;

    [HideInInspector] public StatisticsInfo statisticsInfo;
    public void SetAllStatsToZero()
    {
        slotStats.SetAllStatsToZero();
        mainCharacterStats.statisticsInfo = statisticsInfo; mainCharacterStats.SetAllStatsToZero();
    }
}

public enum GameDifficulty
{
    eazy,
    medium,
    hard,
    crazy
}

[Serializable]
public class SlotStats
{
    public bool gameIsCreated;
    public string gameName;
    public GameDifficulty gameDifficulty;

    public void SetAllStatsToZero()
    {
        gameIsCreated = false;
        gameName = string.Empty;
        gameDifficulty = GameDifficulty.medium;
    }
}

[Serializable]
public class MainCharacterStats
{
    public float health;
    public float energy;
    public float mana;
    public float satiety;
    public int level;
    public int _XP;
    public int statisticPoints;
    public int _HPLevel;
    public int energyLevel;
    public int movementLevel;
    public int _XPMultiplierLevel;
    public int closeCombatLevel;
    public int rangedCombatLevel;
    public int magicCombatLevel;
    public int currentPerspective;
    public float shoulderOffsetZ;
    public float remainingRespawnTime;

    [HideInInspector] public StatisticsInfo statisticsInfo;
    public void SetAllStatsToZero()
    {
        health = statisticsInfo.MaxHPValues[0];
        energy = statisticsInfo.MaxEnergyValues[0];
        mana = statisticsInfo.ÑloseCombatAdditionalManaValues[0] + statisticsInfo.RangedCombatAdditionalManaValues[0] + statisticsInfo.MagicCombatAdditionalManaValues[0];
        satiety = statisticsInfo.SatietyMaxValue;
        level = 0;
        _XP = 0;
        statisticPoints = 0;
        _HPLevel = 0;
        energyLevel = 0;
        movementLevel = 0;
        _XPMultiplierLevel = 0;
        closeCombatLevel = 0;
        rangedCombatLevel = 0;
        magicCombatLevel = 0;
        currentPerspective = 1;
        shoulderOffsetZ = 5f;
        remainingRespawnTime = 0f;
    }
}