using System.IO;
using UnityEngine;

public class StartGameAndWhatMustBeDownLoad : MonoBehaviour
{
    private string pathKeyBinds;
    private string pathSettings;

    private SaveSetting ss;
    private Binds binds;
    private void Awake()
    {
        pathKeyBinds = $"{Application.persistentDataPath}/KeyBinds.json";
        pathSettings = $"{Application.persistentDataPath}/Settings.json";
        binds = new();
        ss = new();

        
    }
    void Start()
    {
        if (!File.Exists(pathKeyBinds) || !File.Exists(pathSettings))
        {
            binds.SetStandartKeyBindWithoutText();
            ss.SetStandartSettings();
            string json = JsonUtility.ToJson(binds);
            File.WriteAllText(pathKeyBinds, json);
            json = JsonUtility.ToJson(ss);
            File.WriteAllText(pathSettings, json);
            Debug.Log("1");
        }
    }
}
