using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene("Setting");
    }
    private void Awake()
    {
        path = $"{Application.persistentDataPath}/KeyBinds.json";
        binds = JsonUtility.FromJson<Binds>(File.ReadAllText(path));
        kbn = binds.allBinds;
        KeyBindsName(kbn);
    }
    private void Start()
    {
        Cursor.visible = false;
        string cursorPath = $"{Application.persistentDataPath}/Settings.json";
        SaveSetting ss = new();
        ss = JsonUtility.FromJson<SaveSetting>(File.ReadAllText(cursorPath));
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

        if (cursor.gameObject.active == true)
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
        string json = JsonUtility.ToJson(bind);
        File.WriteAllText(path,json);
    }
}

[Serializable]
public class Binds
{
    public KeyBindsNames[] allBinds = new KeyBindsNames[2];

    private KeyCode[] standartBind = {KeyCode.Mouse0,KeyCode.W};
    private string[] standartString = { "LMB", "W"};
    private string[] standartname = {"Attack", "Forward"};

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
