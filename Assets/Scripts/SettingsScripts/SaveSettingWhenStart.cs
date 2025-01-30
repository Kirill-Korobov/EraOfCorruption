using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

public class SaveSettingWhenStart : MonoBehaviour
{
    private void Awake()
    {
        if (File.Exists($"{Application.persistentDataPath}/Settings.json") && File.Exists($"{Application.persistentDataPath}/KeyBinds.json"))
        {
            SaveSetting ss = new SaveSetting();
            ss.SetStandartSettings();
            using (var writer = new StreamWriter($"{Application.persistentDataPath}/Settings.json"))
            {
                writer.WriteLine(JsonUtility.ToJson(ss));
            }
            LoadedSettings.LoadSettings(ss);
            Binds binds = new Binds();
            binds.SetStandartKeyBindWithoutText();

            using (var writer = new StreamWriter($"{Application.persistentDataPath}/KeyBinds.json"))
            {
                writer.WriteLine(JsonUtility.ToJson(binds));
            }
        }
    }
}
