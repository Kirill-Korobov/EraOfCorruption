using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class LoadScripts_Meneger : MonoBehaviour
{
    private SaveSetting ss;
    private KeyBindsNames[] kbn;

    public Image imageCursor;
    private void Awake()
    {
        LoadedSettings.lsm = this;


        string path = $"{Application.persistentDataPath}/Settings.json";
        string json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        ss = JsonUtility.FromJson<SaveSetting>(json);
        path = $"{Application.persistentDataPath}/KeyBinds.json";
        json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        kbn = JsonUtility.FromJson<Binds>(json).allBinds;
        Load(ss);
        LoadBinds(kbn);
    }
    private void Start()
    {
    }
    public void Load(SaveSetting ss)
    {
        Sprite sp;
        LoadedSettings.extension = ss.extension;
        if (ss.customCursor)
        {
            Texture2D tx = new Texture2D(2, 2);
            tx.LoadImage(Convert.FromBase64String(ss.imageCursor));
            Rect rt = new Rect(0, 0, tx.width, tx.height);
            sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

            LoadedSettings.imageCursor = sp;
        }
        else
        {
            LoadedSettings.imageCursor = null;

            imageCursor.gameObject.SetActive(false);
        }
        LoadedSettings.mapPause = ss.mapPause;
        LoadedSettings.statsPause = ss.statsPause;
        LoadedSettings.questsPause = ss.questsPause;
        LoadedSettings.inventoryPause = ss.inventoryPause;
        LoadedSettings.customCursor = ss.customCursor;
        LoadedSettings.cursorSizes = ss.cursorSizes;
        LoadedSettings.song = ss.song;
        LoadedSettings.music = ss.music;
        LoadedSettings.sensivity = ss.sensivity;
        LoadedSettings.muteSongs = ss.mute;
    }
    public void LoadBinds(KeyBindsNames[] kbn)
    {
        
        LoadedSettings.attack = kbn[0].bind;
        LoadedSettings.mute = kbn[1].bind;
        LoadedSettings.forward = kbn[2].bind;
        LoadedSettings.right = kbn[3].bind;
        LoadedSettings.left = kbn[4].bind;
        LoadedSettings.back = kbn[5].bind;
        LoadedSettings.run = kbn[6].bind;
        LoadedSettings.jump = kbn[7].bind;
        LoadedSettings.dash = kbn[8].bind;
        LoadedSettings.teleport = kbn[9].bind;
        LoadedSettings.take = kbn[10].bind;
        LoadedSettings.drop = kbn[11].bind;
        LoadedSettings.inventory1 = kbn[12].bind;
        LoadedSettings.inventory2 = kbn[13].bind;
        LoadedSettings.inventory3 = kbn[14].bind;
        LoadedSettings.inventory4 = kbn[15].bind;
        LoadedSettings.inventory5 = kbn[16].bind;
        LoadedSettings.inventory6 = kbn[17].bind;
        LoadedSettings.inventory7 = kbn[18].bind;
        LoadedSettings.inventory8 = kbn[19].bind;
        LoadedSettings.inventory9 = kbn[20].bind;
        LoadedSettings.inventory0 = kbn[21].bind;
        LoadedSettings.openInventory = kbn[22].bind;
        LoadedSettings.openMenu = kbn[23].bind;
        LoadedSettings.openStats = kbn[24].bind;
        LoadedSettings.openQuests = kbn[25].bind;
        LoadedSettings.npc = kbn[26].bind;
        LoadedSettings.escape = kbn[27].bind;
    }


}
