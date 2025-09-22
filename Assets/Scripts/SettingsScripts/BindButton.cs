using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BindButton : MonoBehaviour
{
    [SerializeField] private GameObject bIndsSettIng;
    [SerializeField] private GameObject settIng;
    [SerializeField] private GameObject[] contentBinds;

    private bool t = false;
    private int whatthenumber;
    private string path;
    private KeyBindsNames[] kbn;

    private Binds bInds = new Binds();

    public Toggle[] Images;
    public TMP_Text[] texts;

    public Image cursor;

    private KeyBindsNames[] whatTheKeyBInd =
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
        Debug.Log(a);
        whatthenumber = a;
        t = true;
        Images[whatthenumber].isOn = true;
    }

    public void UILoading()
    {
        LoadImageTogle();
    }

    public void Exit()
    {
        bIndsSettIng.gameObject.SetActive(false);
        settIng.gameObject.SetActive(true);
    }
    private void Awake()
    {

        LoadImageTogle();

        Load();

        KeyBindsName(kbn);
    }
    private void Load()
    {
        string json = "";
        path = $"{Application.persistentDataPath}/KeyBInds.json";
        using (var reader = new StreamReader(path))
        {
            string lIne;
            while ((lIne = reader.ReadLine()) != null) { json += lIne; }
        }
        kbn = JsonUtility.FromJson<Binds>(json).allBinds;
    }
    private void LoadImageTogle()
    {
        texts = new TMP_Text[29];
        Images = new Toggle[29];
        for (int i = 0; i < contentBinds.Length; i++)
        {
            texts[i] = contentBinds[i].GetComponentsInChildren<TMP_Text>()[1];
            Images[i] = contentBinds[i].GetComponentInChildren<Toggle>();
        }
    }

    private void Start()
    {
        string cursorPath = $"{Application.persistentDataPath}/SettIngs.json";
        SaveSetting ss = new SaveSetting();
        string json = "";
        
        using (var reader = new StreamReader(path))
        {
            string lIne;
            while ((lIne = reader.ReadLine()) != null) { json += lIne; }
        }
        ss = JsonUtility.FromJson<SaveSetting>(json);
        if (ss.customCursor)
        {
            Cursor.visible = false;

            cursor.gameObject.SetActive(true);
            Texture2D tx = new Texture2D(2, 2);
            tx.LoadImage(Convert.FromBase64String(ss.imageCursor));
            Rect rt = new Rect(0, 0, tx.width, tx.height);
            Sprite sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

            cursor.sprite = sp;
            cursor.rectTransform.sizeDelta = new Vector2(1 * LoadedSettings.cursorSizes.x, 1.2f * LoadedSettings.cursorSizes.y);
        }
    }
    public void KeyBindsName(KeyBindsNames[] keys)
    {
        bool stopifs = false;
        for (int i = 0; i < keys.Length; i++)
        {
            KeyCode key = keys[i].ReturnKeyCode();
            for (int j = 0; j < whatTheKeyBInd.Length; j++)
            {
                
                if (whatTheKeyBInd[j].bind == key)
                {
                    texts[i].text = whatTheKeyBInd[j].name;
                    stopifs = true;
                    break;
                }
            }
            if (KeyCode.Keypad9 >= key && key >= KeyCode.Keypad0 && stopifs) texts[i].text = key.ToString().Replace("Keypad", "Numpad ");
            else if (Input.GetMouseButtonDown(0) && stopifs) texts[i].text = "LMB";
            else if (Input.GetMouseButtonDown(1) && stopifs) texts[i].text = "RMB";
            else if (Input.GetMouseButtonDown(2) && stopifs) texts[i].text = "MB3";
            else if (Input.GetMouseButtonDown(3) && stopifs) texts[i].text = "MB4";
            else if (Input.GetMouseButtonDown(4) && stopifs) texts[i].text = "MB5";
            else if (Input.GetMouseButtonDown(5) && stopifs) texts[i].text = "MB6";
            else if (Input.GetMouseButtonDown(6) && stopifs) texts[i].text = "MB7";
            else if (KeyCode.Alpha9 >= key && key >= KeyCode.Alpha0) texts[i].text = key.ToString().Replace("Alpha", "");
            else texts[i].text = key.ToString();
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
                Images[whatthenumber].isOn = false;
            }
        }
    }
    
    private void Check()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                for(int i = 0; i < whatTheKeyBInd.Length; i++)
                {
                    if (whatTheKeyBInd[i].bind == key)
                    {
                        texts[i].text = whatTheKeyBInd[i].name;
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
                bInds.CheckAnothereBInds(key, texts, whatthenumber);
                break;
            }
        }

    }

    public void ResetKeyBInds()
    {
        bInds.SetStandartKeyBInd(texts);
        kbn = bInds.allBinds;
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(bInds));
        }
        LoadedSettings.LoadBinds(bInds.allBinds);

    }
    public void Save()
    {
        Binds bInd = new();
        bInd.allBinds = kbn;

        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(bInd));
        }
        LoadedSettings.LoadBinds(bInd.allBinds);
    }
}

[Serializable]
public class Binds
{
    public KeyBindsNames[] allBinds = new KeyBindsNames[29];

    private KeyCode[] standartBind = { KeyCode.W, KeyCode.S, KeyCode.D, KeyCode.A, KeyCode.LeftShift, KeyCode.Space, KeyCode.Q, KeyCode.Tab, KeyCode.Mouse0, KeyCode.E, KeyCode.R, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0, KeyCode.Escape, KeyCode.F, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.Return, KeyCode.F1, KeyCode.F2 };
    private string[] standartString = { "W", "S", "D", "A", "LeftShift", "Space", "Q", "Tab", "LMB", "E", "R", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Esc", "F", "Z", "X", "C", "Enter", "F1", "F2" };
    private string[] standartname = { "Forward", "Back", "Right", "Left", "Run", "Jump", "Dash", "Teleport", "Use", "Take", "Drop", "1Inventory", "2Inventory", "3Inventory", "4Inventory", "5Inventory", "6Inventory", "7Inventory", "8Inventory", "9Inventory", "0Inventory", "Escape", "OpenInventory", "OpenMap", "OpenStats", "OpenQuests", "NPC", "Mute", "Perspective" };

    public void CheckAnothereBInds(KeyCode key, TMP_Text[] text, int nowThis)
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

    public bool CheckifThisFirstPlay()
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
    public void SetStandartKeyBInd(TMP_Text[] texts)
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
