using System;
using System.IO;
using TMPro;
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
        for (int i = 0; i < keys.Length; i++)
        {
            KeyCode key = keys[i].ReturnKeyCode();
            if (key == KeyCode.Mouse0) texts[i].text = "LMB";
            else if (key == KeyCode.Mouse1) texts[i].text = "RMB";
            else if (key == KeyCode.Mouse2) texts[i].text = "MB3";
            else if (key == KeyCode.RightBracket) texts[i].text = "[";
            else if (key == KeyCode.LeftBracket) texts[i].text = "]";
            else if (key == KeyCode.RightBracket) texts[i].text = "[";
            else if (key == KeyCode.Semicolon) texts[i].text = ";";
            else if (key == KeyCode.Quote) texts[i].text = "'";
            else if (key == KeyCode.Backslash) texts[i].text = "\\";
            else if (key == KeyCode.Comma) texts[i].text = ",";
            else if (key == KeyCode.Period) texts[i].text = ".";
            else if (key == KeyCode.Slash) texts[i].text = "/";
            else if (key == KeyCode.Return) texts[i].text = "Enter";
            else if (key == KeyCode.BackQuote) texts[i].text = "`";
            else if (key == KeyCode.Minus) texts[i].text = "-";
            else if (key == KeyCode.Equals) texts[i].text = "=";
            else if (KeyCode.Keypad9 >= key && key >= KeyCode.Keypad0) texts[i].text = key.ToString().Replace("Keypad", "Numpad ");
            else if (KeyCode.Alpha9 >= key && key >= KeyCode.Alpha0) texts[i].text = key.ToString().Replace("Alpha", "");
            else if (key == KeyCode.KeypadEquals) texts[i].text = "*";
            else if (key == KeyCode.KeypadDivide) texts[i].text = "Numpad /";
            else if (key == KeyCode.KeypadPlus) texts[i].text = "Numpad +";
            else if (key == KeyCode.KeypadMinus) texts[i].text = "Numpad -";
            else if (key == KeyCode.KeypadEnter) texts[i].text = "Numpad Enter";
            else if (key == KeyCode.KeypadPeriod) texts[i].text = "Numpad .";
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
                if (Input.GetMouseButtonDown(0)) texts[whatthenumber].text = "LMB";
                else if (Input.GetMouseButtonDown(1)) texts[whatthenumber].text = "RMB";
                else if (Input.GetMouseButtonDown(2)) texts[whatthenumber].text = "MB3";
                else if (key == KeyCode.Mouse3) texts[whatthenumber].text = "MB4";
                else if (key == KeyCode.Mouse4) texts[whatthenumber].text = "MB5";
                else if (key == KeyCode.Mouse5) texts[whatthenumber].text = "MB6";
                else if (key == KeyCode.Mouse6) texts[whatthenumber].text = "MB7";
                else if (key == KeyCode.RightBracket) texts[whatthenumber].text = "[";
                else if (key == KeyCode.LeftBracket) texts[whatthenumber].text = "]";
                else if (key == KeyCode.RightBracket) texts[whatthenumber].text = "[";
                else if (key == KeyCode.Semicolon) texts[whatthenumber].text = ";";
                else if (key == KeyCode.Quote) texts[whatthenumber].text = "'";
                else if (key == KeyCode.Backslash) texts[whatthenumber].text = "\\";
                else if (key == KeyCode.Comma) texts[whatthenumber].text = ",";
                else if (key == KeyCode.Period) texts[whatthenumber].text = ".";
                else if (key == KeyCode.Slash) texts[whatthenumber].text = "/";
                else if (key == KeyCode.Return) texts[whatthenumber].text = "Enter";
                else if (key == KeyCode.BackQuote) texts[whatthenumber].text = "`";
                else if (key == KeyCode.Minus) texts[whatthenumber].text = "-";
                else if (key == KeyCode.Equals) texts[whatthenumber].text = "=";
                else if (KeyCode.Keypad9 >= key && key >= KeyCode.Keypad0) texts[whatthenumber].text = key.ToString().Replace("Keypad", "Numpad ");
                else if (KeyCode.Alpha9 >= key && key >= KeyCode.Alpha0) texts[whatthenumber].text = key.ToString().Replace("Alpha", "");
                else if (key == KeyCode.KeypadEquals) texts[whatthenumber].text = "*";
                else if (key == KeyCode.KeypadDivide) texts[whatthenumber].text = "Numpad /";
                else if (key == KeyCode.KeypadPlus) texts[whatthenumber].text = "Numpad +";
                else if (key == KeyCode.KeypadMinus) texts[whatthenumber].text = "Numpad -";
                else if (key == KeyCode.KeypadEnter) texts[whatthenumber].text = "Numpad Enter";
                else if (key == KeyCode.KeypadPeriod) texts[whatthenumber].text = "Numpad .";
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
