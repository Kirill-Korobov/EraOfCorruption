using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using TMPro;

public class Setting : MonoBehaviour
{
    private float valueMusic;
    private float valueSong;
    private string path;
    public bool ifSpriteIs;
    private SaveSetting ss;

    public TMP_Text[] txts;
    public Toggle mute;
    public Slider music;
    public Slider song;

    public Slider cursorSize;
    public Toggle customCursor;
    public Image importCursor;
    public Image cursor;

    public Slider sensivity;

    public Toggle pause;
    public Toggle inventory;
    public Toggle quests;
    public Toggle map;

    [Obsolete]
    private void Update()
    {
        if (cursor.gameObject.active == true)
        {
            cursor.transform.position = new Vector3(Input.mousePosition.x + (cursor.rectTransform.sizeDelta.x / 2), Input.mousePosition.y - (cursor.rectTransform.sizeDelta.y / 2));
        }
    }
    private void Start()
    {
        path = $"{Application.persistentDataPath}/Settings.json";
        ss = JsonUtility.FromJson<SaveSetting>(File.ReadAllText(path));
        LoadSaves();
    }
    public void LoadSaves()
    {
        song.value = ss.song;
        music.value = ss.music;


        if (ss.music != 0 && ss.song != 0)
        {
            music.value = ss.music;
            song.value = ss.song;
            mute.isOn = false;
        }
        else
        {
            music.gameObject.SetActive(false);
            song.gameObject.SetActive(false);
            txts[0].gameObject.SetActive(false);
            txts[1].gameObject.SetActive(false);
            music.value = ss.musicSaveDontUseHim;
            song.value = ss.songSaveDontUseHim;
        }
        cursor.rectTransform.sizeDelta = ss.cursorSizes;
        cursorSize.value = ss.cursorSizes.x;
        sensivity.value = ss.sensivity;


        if (!ss.inventoryPause && !ss.questsPause && !ss.mapPause)
        {
            inventory.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            map.gameObject.SetActive(false);
            pause.isOn = false;
            inventory.isOn = ss.inventoryPauseSaveDontUseHim;
            quests.isOn = ss.inventoryPauseSaveDontUseHim;
            map.isOn = ss.mapPauseSaveDontUseHim;
        }
        else
        {
            inventory.isOn = ss.inventoryPause;
            quests.isOn = ss.questsPause;
            map.isOn = ss.mapPause;
        }

        if (ss.customCursor)
        {
            importCursor.gameObject.SetActive(true);
            cursor.gameObject.SetActive(true);
            Cursor.visible = false;
        }
        else
        {
            customCursor.isOn = false;
            importCursor.gameObject.SetActive(false);
            cursor.gameObject.SetActive(false);
            Cursor.visible = true;
        }


        if (ss.imageCursor != "")
        {
            Texture2D tx = new Texture2D(2, 2);
            tx.LoadImage(Convert.FromBase64String(ss.imageCursor));
            Rect rt = new Rect(0, 0, tx.width, tx.height);
            Sprite sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

            cursor.sprite = sp;
        }
        else
        {
            ifSpriteIs = true;
            cursor.sprite = null;
        }
    }
    public void CustomCursor()
    {
        Debug.Log(1);
        if (ifSpriteIs && customCursor.isOn != false)
        {
            Debug.Log(2);
            string path = EditorUtility.OpenFilePanel("", "", "png,jpg");


            string extension = Path.GetExtension(path).ToLower();

            if (extension != ".png" && extension != ".jpg")
            {
                customCursor.isOn = false;
            }
            else if (path.Length != 0)
            {
                Texture2D tx = new Texture2D(2, 2);
                tx.LoadImage(File.ReadAllBytes(path));
                Rect rt = new Rect(0, 0, tx.width, tx.height);
                Sprite sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

                cursor.sprite = sp;
                ifSpriteIs = false;

                Debug.Log(3);
                if (customCursor.isOn)
                {
                    importCursor.gameObject.SetActive(true);
                    cursor.gameObject.SetActive(true);
                    Cursor.visible = false;
                }
                else
                {
                    importCursor.gameObject.SetActive(false);
                    cursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                }
            }
            else
            {
                customCursor.isOn = false;
            } 
        }
        else
        {
            if (customCursor.isOn)
            {
                importCursor.gameObject.SetActive(true);
                cursor.gameObject.SetActive(true);
                Cursor.visible = false;
            }
            else
            {
                importCursor.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
    }

    public void AddNewCustomCursor()
    {
        string path = EditorUtility.OpenFilePanel("","", "png,jpg");

        string extension = Path.GetExtension(path).ToLower();

        if (extension != ".png" && extension != ".jpg")
        {
            customCursor.isOn = false;
        }
        if (path.Length != 0)
        {
            Texture2D tx = new Texture2D(2, 2);
            tx.LoadImage(File.ReadAllBytes(path));
            Rect rt = new Rect(0, 0, tx.width, tx.height);
            Sprite sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

            cursor.sprite = sp;
        }
    }

    public void Binds()
    {
        SceneManager.LoadScene("SettingBinds");
    }

    public void Mute()
    {
        if (!mute.isOn)
        {
            music.gameObject.SetActive(true);
            song.gameObject.SetActive(true);
            valueMusic = music.value;
            valueSong = song.value;
            txts[0].gameObject.SetActive(true);
            txts[1].gameObject.SetActive(true);
        }
        else
        {
            music.gameObject.SetActive(false);
            song.gameObject.SetActive(false);
            txts[0].gameObject.SetActive(false);
            txts[1].gameObject.SetActive(false);
            valueMusic = 0;
            valueSong = 0;
        }

    }
    public void Music()
    {
        valueMusic = music.value;
    }
    public void Song()
    {
        valueSong = song.value;
    }
    public void CursorSize()
    {
        cursor.rectTransform.sizeDelta = new Vector2(1*cursorSize.value,1.2f*cursorSize.value);
    }
    public void Pause()
    {
        if (pause.isOn)
        {
            inventory.gameObject.SetActive(true);
            quests.gameObject.SetActive(true);
            map.gameObject.SetActive(true);
        }
        else
        {
            inventory.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            map.gameObject.SetActive(false);
        }
        
    }
    public void ResetSettings()
    {
        ss.SetStandartSettings();
        LoadSaves();
        Save();
    }

    public void Save()
    {
        if (!mute.isOn)
        {
            ss.song = song.value;
            ss.music = music.value;
        }
        else
        {
            ss.song = 0;
            ss.music = 0;
            ss.songSaveDontUseHim = song.value;
            ss.musicSaveDontUseHim = music.value;
        }
        ss.cursorSizes = cursor.rectTransform.sizeDelta;
        ss.sensivity = sensivity.value;
        if (!pause.isOn)
        {
            ss.inventoryPause = false;
            ss.questsPause = false;
            ss.mapPause = false;
            ss.inventoryPauseSaveDontUseHim = inventory.isOn;
            ss.questsPauseSaveDontUseHim = quests.isOn;
            ss.mapPauseSaveDontUseHim = map.isOn;
        }
        else
        {
            ss.inventoryPause = inventory.isOn;
            ss.questsPause = quests.isOn;
            ss.mapPause = map.isOn;
        }
        ss.customCursor = customCursor.isOn;
        string extension = Path.GetExtension(path).ToLower();
        if (extension == ".png")
            ss.imageCursor = Convert.ToBase64String(cursor.sprite.texture.EncodeToPNG());
        else if (extension == ".jpg")
            ss.imageCursor = Convert.ToBase64String(cursor.sprite.texture.EncodeToJPG());

        string json = JsonUtility.ToJson(ss);
        File.WriteAllText(path, json);
    }
}

public class SaveSetting
{
    public float song;
    public float music;
    public float songSaveDontUseHim;
    public float musicSaveDontUseHim;
    public Vector2 cursorSizes;
    public float sensivity;
    public bool inventoryPause;
    public bool questsPause;
    public bool mapPause;
    public bool inventoryPauseSaveDontUseHim;
    public bool questsPauseSaveDontUseHim;
    public bool mapPauseSaveDontUseHim;
    public bool customCursor;
    public string imageCursor;

    public void SetStandartSettings()
    {
        song = 70f;
        music = 70f;
        cursorSizes = new Vector2(50,60);
        sensivity = 70f;
        inventoryPause = false;
        questsPause = false;
        mapPause = false;
        customCursor = false;
        imageCursor = "";
    }
} 
