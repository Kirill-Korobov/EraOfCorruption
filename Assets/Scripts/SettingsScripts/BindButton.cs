using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BindButton : MonoBehaviour
{
    private bool t = false;
    private int whatthenumber;
    private string path;
    private KeyBindsNames[] kbn;

    private Binds binds = new Binds();

    public Image[] images;
    public TMP_Text[] texts;

    public Image cursor;

    private void OnApplicationQuit()
    {
        Save();
    }
    private KeyBindsNames[] whatTheKeyBind =
                {
                    new KeyBindsNames(KeyCode.Mouse0, "LMB"),
                    new KeyBindsNames(KeyCode.Mouse1, "RMB"),
                    new KeyBindsNames(KeyCode.Mouse2, "MB3"),
                    new KeyBindsNames(KeyCode.Mouse3, "MB4"),
                    new KeyBindsNames(KeyCode.Mouse4, "MB5"),
                    new KeyBindsNames(KeyCode.Mouse5, "MB6"),
                    new KeyBindsNames(KeyCode.Mouse6, "MB7"),
                    new KeyBindsNames(KeyCode.RightBracket, "["),
                    new KeyBindsNames(KeyCode.LeftBracket, "]"),
                    new KeyBindsNames(KeyCode.Semicolon, ";"),
                    new KeyBindsNames(KeyCode.Quote, "'"),
                    new KeyBindsNames(KeyCode.Backslash, "\\"),
                    new KeyBindsNames(KeyCode.Comma, ","),
                    new KeyBindsNames(KeyCode.Period, "."),
                    new KeyBindsNames(KeyCode.Slash, "/"),
                    new KeyBindsNames(KeyCode.Return, "Enter"),
                    new KeyBindsNames(KeyCode.BackQuote, "`"),
                    new KeyBindsNames(KeyCode.Minus, "-"),
                    new KeyBindsNames(KeyCode.Equals, "="),
                    new KeyBindsNames(KeyCode.KeypadEquals, "*"),
                    new KeyBindsNames(KeyCode.KeypadDivide, "Numpad /"),
                    new KeyBindsNames(KeyCode.KeypadPlus, "Numpad +"),
                    new KeyBindsNames(KeyCode.KeypadMinus, "Numpad -"),
                    new KeyBindsNames(KeyCode.KeypadEnter, "Numpad Enter"),
                    new KeyBindsNames(KeyCode.KeypadPeriod,"Numpad .")
                };

    public void Clicked(int a)
    {
        whatthenumber = a;
        t = true;
        images[whatthenumber].gameObject.SetActive(true);
    }
    /*
    public void PBFDG()
    {
        
        for (int i = 0; i < binds.allBinds.Length; i++)
        {
            if (binds.allBinds[i].ReturnString() == "Attack")
            {
                KeyCode attack = binds.allBinds[i].ReturnKeyCode();
            }
        }
    }
    */
    public void Exit()
    {
        //idk
    }
    private void Awake()
    {
        string json = "";
        path = $"{Application.persistentDataPath}/KeyBinds.json";
        binds.SetStandartKeyBindWithoutText();
        kbn = binds.allBinds;
        Save();
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        kbn = JsonUtility.FromJson<Binds>(json).allBinds;
    }
    private void Start()
    {
        Cursor.visible = false;
        string cursorPath = $"{Application.persistentDataPath}/Settings.json";
        SaveSetting ss = new SaveSetting();
        string json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        ss = JsonUtility.FromJson<SaveSetting>(json);
        if (ss.customCursor)
        {
            Texture2D tx = new Texture2D(2, 2);
            tx.LoadImage(Convert.FromBase64String(ss.imageCursor));
            Rect rt = new Rect(0, 0, tx.width, tx.height);
            Sprite sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

            cursor.sprite = sp;
        }
        cursor.rectTransform.sizeDelta = ss.cursorSizes;
    }
    public void KeyBindsName(KeyBindsNames[] keys)
    {
        bool stopIfs = false;
        for (int i = 0; i < keys.Length; i++)
        {
            KeyCode key = keys[i].ReturnKeyCode();
            for (int j = 0; j < whatTheKeyBind.Length; j++)
            {
                if (whatTheKeyBind[j].bind == key)
                {
                    texts[whatthenumber].text = whatTheKeyBind[j].name;
                    stopIfs = true;
                    break;
                }
            }
            if (KeyCode.Keypad9 >= key && key >= KeyCode.Keypad0 && stopIfs) texts[whatthenumber].text = key.ToString().Replace("Keypad", "Numpad ");
            else if (Input.GetMouseButtonDown(0) && stopIfs) texts[whatthenumber].text = "LMB";
            else if (Input.GetMouseButtonDown(1) && stopIfs) texts[whatthenumber].text = "RMB";
            else if (Input.GetMouseButtonDown(2) && stopIfs) texts[whatthenumber].text = "MB3";
            else if (Input.GetMouseButtonDown(3) && stopIfs) texts[whatthenumber].text = "MB4";
            else if (Input.GetMouseButtonDown(4) && stopIfs) texts[whatthenumber].text = "MB5";
            else if (Input.GetMouseButtonDown(5) && stopIfs) texts[whatthenumber].text = "MB6";
            else if (Input.GetMouseButtonDown(6) && stopIfs) texts[whatthenumber].text = "MB7";
            else if (KeyCode.Alpha9 >= key && key >= KeyCode.Alpha0) texts[whatthenumber].text = key.ToString().Replace("Alpha", "");
            else texts[whatthenumber].text = key.ToString();
        }
    }

    private void Update()
    {
        if(t)
        {
            if (Input.anyKeyDown)
            {
                Check();
                t = false;
                images[whatthenumber].gameObject.SetActive(false);
            }
        }

        if (cursor.gameObject.activeSelf == true)
        {
            cursor.transform.position = new Vector3(Input.mousePosition.x + (cursor.rectTransform.sizeDelta.x / 2), Input.mousePosition.y - (cursor.rectTransform.sizeDelta.y / 2));
        }
    }
    
    private void Check()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                for(int i = 0; i < whatTheKeyBind.Length; i++)
                {
                    if (whatTheKeyBind[i].bind == key)
                    {
                        texts[whatthenumber].text = whatTheKeyBind[i].name;
                    }
                }
                if (KeyCode.Keypad9 >= key && key >= KeyCode.Keypad0) texts[whatthenumber].text = key.ToString().Replace("Keypad", "Numpad ");
                else if (Input.GetMouseButtonDown(0)) texts[whatthenumber].text = "LMB";
                else if (Input.GetMouseButtonDown(1)) texts[whatthenumber].text = "RMB";
                else if (Input.GetMouseButtonDown(2)) texts[whatthenumber].text = "MB3";
                else if (Input.GetMouseButtonDown(3)) texts[whatthenumber].text = "MB4";
                else if (Input.GetMouseButtonDown(4)) texts[whatthenumber].text = "MB5";
                else if (Input.GetMouseButtonDown(5)) texts[whatthenumber].text = "MB6";
                else if (Input.GetMouseButtonDown(6)) texts[whatthenumber].text = "MB7";
                else if (KeyCode.Alpha9 >= key && key >= KeyCode.Alpha0) texts[whatthenumber].text = key.ToString().Replace("Alpha", "");
                else texts[whatthenumber].text = key.ToString();

                kbn[whatthenumber].SaveKeyCode(key);
                binds.CheckAnothereBinds(key, texts, whatthenumber);
                break;
            }
        }

    }

    public void ResetKeyBinds()
    {
        binds.SetStandartKeyBind(texts);
        Save();
    }
    public void Save()
    {
        Binds bind = new();
        bind.allBinds = kbn;

        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(bind));
        }
        LoadedSettings.LoadBinds(bind.allBinds);
    }
}

[Serializable]
public class Binds
{
    public KeyBindsNames[] allBinds = new KeyBindsNames[28];

    private KeyCode[] standartBind = { KeyCode.Mouse0, KeyCode.F1, KeyCode.W, KeyCode.D, KeyCode.A, KeyCode.S, KeyCode.LeftShift, KeyCode.Space, KeyCode.Q, KeyCode.Tab, KeyCode.E, KeyCode.R, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.F, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.Return, KeyCode.Escape };
    private string[] standartString = { "LMB", "F1", "W", "D", "A", "S", "LeftShift", "Space", "Q", "Tab", "E", "R", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "F", "Z", "X", "C", "Enter", "Esc" };

    private string[] standartname = {"Attack", "Mute", "Forward", "Right", "Left", "Back", "Run","Jump", "Dash", "Teleport", "Take", "Drop", "1Inventory", "2Inventory", "3Inventory", "4Inventory", "5Inventory", "6Inventory", "7Inventory", "8Inventory", "9Inventory", "0Inventory", "OpenInventory", "OpenMenu", "OpenStats", "OpenQuests", "NPC", "Escape"};
    

    public void CheckAnothereBinds(KeyCode key, TMP_Text[] text, int nowThis)
    {
        for (int i = 0; i < allBinds.Length; i++)
        {
            if (i != nowThis &&  key == allBinds[i].ReturnKeyCode())
            {
                allBinds[i].SaveKeyCode(KeyCode.None);
                text[i].text = null;
            }
        }
    }

    public bool CheckIfThisFirstPlay()
    {
        int howMuchDontSave = 0;
        int i;
        for (i = 0;  i < allBinds.Length; i++)
        {
            if (KeyCode.None == allBinds[i].ReturnKeyCode())
            {
                howMuchDontSave++;
            }
        }
        if ( howMuchDontSave == i)
        {
            return true;
        }
        return false;
    }
    public void SetStandartKeyBind(TMP_Text[] texts)
    {
        for(int i = 0; i <standartBind.Length; i++)
        {
            allBinds[i].Save(standartBind[i], standartname[i]);
            texts[i].text = standartString[i];
        }
    }
    public void SetStandartKeyBindWithoutText()
    {
        for (int i = 0; i < standartBind.Length; i++)
        {
            allBinds[i].Save(standartBind[i], standartname[i]);
        }
    }

}
