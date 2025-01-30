using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadTimersOnGO : MonoBehaviour
{
    [SerializeField] private TakeItem[] go;
    void Awake()
    {
        string path = $"{Application.persistentDataPath}/Timers.json";
        string json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        List<int> st = JsonUtility.FromJson<SaveTimers>(json).ints;

        for (int i = 0; i < go.Length; i++)
        {
            go[i].reloadIfLets = st[i];
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    public void Save()
    {
        SaveTimers ts = new SaveTimers();

        for (int i = 0; i < go.Length; i++)
        {
            ts.ints.Add(go[i].reloadIfLets);
        }
        string path = $"{Application.persistentDataPath}/Timers.json";
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(ts));
        }
    }
    public class SaveTimers
    {
        public List<int> ints = new List<int>();
    }
}
