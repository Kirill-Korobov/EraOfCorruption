using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static void Initialize()
    {
         // Initialize.
    }

    public enum GameDifficulty
    {
        eazy,
        medium,
        hard,
        crazy
    }

    public static bool game1_GameIsCreated;
    public static string game1_GameName;
    public static GameDifficulty game1_Difficulty;

    public static bool game2_GameIsCreated;
    public static string game2_GameName;
    public static GameDifficulty game2_Difficulty;

    public static bool game3_GameIsCreated;
    public static string game3_GameName;
    public static GameDifficulty game3_Difficulty;
}