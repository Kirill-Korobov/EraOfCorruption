using System;
using System.IO;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    private string game1StatsSavePath, game2StatsSavePath, game3StatsSavePath;
    public GameStats game1Stats, game2Stats, game3Stats;

    private void Awake()
    {
        game1StatsSavePath = $"{Application.persistentDataPath}/Game1Stats.json";
        game2StatsSavePath = $"{Application.persistentDataPath}/Game2Stats.json";
        game3StatsSavePath = $"{Application.persistentDataPath}/Game3Stats.json";

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
}

public enum GameDifficulty
{
    eazy,
    medium,
    hard,
    crazy
}

[Serializable]
public class GameStats
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