using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MuteManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] SoundManager soundManager;
    private KeyCode keyCode;
    private void Start()
    {
        keyCode = LoadedSettings.mute;
    }
    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            Mute();
        }
    }

    public void Mute()
    {
        SaveSetting ss = new SaveSetting();

        ss.mapPause = LoadedSettings.mapPause;
        ss.statsPause = LoadedSettings.statsPause;
        ss.questsPause = LoadedSettings.questsPause;
        ss.inventoryPause = LoadedSettings.inventoryPause;
        ss.customCursor = LoadedSettings.customCursor;
        ss.cursorSizes = LoadedSettings.cursorSizes;
        if (ss.mute)
        {
            ss.song = 0;
            ss.music = 0;
            LoadedSettings.music = 0;
            LoadedSettings.song = 0;
        }
        else
        {
            ss.song = LoadedSettings.song;
            ss.music = LoadedSettings.music;
        }
        musicManager.ChangeVolume(ss.music);
        soundManager.ChangeVolume(ss.song);
        ss.sensivity = LoadedSettings.sensivity;
        using (var writer = new StreamWriter($"{Application.persistentDataPath}/Settings.json"))
        {
            writer.WriteLine(JsonUtility.ToJson(ss));
        }

    }
}
