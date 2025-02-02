using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LoadedSettings
{
    public static bool ifInventoryOpen;
    public static bool ifMapOpen;
    public static bool ifQuestsOpen;
    public static bool ifStatsOpen;
    public static bool ifAnyOpen;

    public static float song;
    public static float music;
    public static Vector2 cursorSizes;
    public static float sensivity;
    public static bool inventoryPause;
    public static bool questsPause;
    public static bool mapPause;
    public static bool statsPause;
    public static bool customCursor;
    public static Sprite imageCursor;
    public static string extension;
    public static bool muteSongs;
    public static bool pause;
    public static LoadScripts_Meneger lsm;

    public static KeyCode attack;
    public static KeyCode mute;
    public static KeyCode forward;
    public static KeyCode right;
    public static KeyCode left;
    public static KeyCode back;
    public static KeyCode run;
    public static KeyCode jump;
    public static KeyCode dash;
    public static KeyCode teleport;
    public static KeyCode take;
    public static KeyCode drop;
    public static KeyCode inventory1;
    public static KeyCode inventory2;
    public static KeyCode inventory3;
    public static KeyCode inventory4;
    public static KeyCode inventory5;
    public static KeyCode inventory6;
    public static KeyCode inventory7;
    public static KeyCode inventory8;
    public static KeyCode inventory9;
    public static KeyCode inventory0;
    public static KeyCode openInventory;
    public static KeyCode openMenu;
    public static KeyCode openStats;
    public static KeyCode openQuests;
    public static KeyCode npc;
    public static KeyCode escape;

    public static void LoadSettings(SaveSetting saveSetting)
    {
        lsm.Load(saveSetting);
    }
    public static void LoadBinds(KeyBindsNames[] kbn)
    {
        lsm.LoadBinds(kbn);
    }
}
