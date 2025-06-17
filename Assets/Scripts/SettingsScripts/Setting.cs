using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using SFB;
using Unity.VisualScripting;
using System.Runtime.Serialization.Formatters.Binary;

public class Setting : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Save();
    }
    [SerializeField] private GameObject binds;
    [SerializeField] private GameObject setting;
    private float valueMusic;
    private float valueSong;
    private string path;
    private string[] pathFile;
    private bool ifSpriteIs;
    private SaveSetting ss;
    private Sprite sp;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private SoundManager soundManager;

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
    public Toggle stats;

    [Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(LoadedSettings.mute))
        {
            Mute();
            Save();
        }
        
    }
    private void Start()
    {
        path = $"{Application.persistentDataPath}/Settings.json";
        string json = "";
        using (var reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null) { json += line; }
        }
        ss = JsonUtility.FromJson<SaveSetting>(json);
        LoadSaves();
    }
    public void LoadSaves()
    {
        song.value = ss.song;
        music.value = ss.music;
        


        if (!ss.mute)
        {
            music.value = ss.music;
            song.value = ss.song;
            mute.isOn = false;
        }
        else
        {
            music.gameObject.SetActive(false);
            song.gameObject.SetActive(false);
            music.value = ss.musicSaveDontUseHim;
            song.value = ss.songSaveDontUseHim;
        }
        musicManager.ChangeVolume(ss.music);
        soundManager.ChangeVolume(ss.song);
        cursor.rectTransform.sizeDelta = ss.cursorSizes;
        cursorSize.value = ss.cursorSizes.x;
        sensivity.value = ss.sensivity;


        if (pause)
        {
            inventory.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            map.gameObject.SetActive(false);
            stats.gameObject.SetActive(false);
            pause.isOn = false;
            inventory.isOn = ss.inventoryPauseSaveDontUseHim;
            quests.isOn = ss.questsPauseSaveDontUseHim;
            map.isOn = ss.mapPauseSaveDontUseHim;
            stats.isOn = ss.statsPauseSaveDontUseHim;
        }
        else
        {
            inventory.isOn = ss.inventoryPause;
            quests.isOn = ss.questsPause;
            map.isOn = ss.mapPause;
            stats.isOn = ss.statsPause;
        }
        if (ss.customCursor)
        {
            Cursor.visible = false;
            cursor.gameObject.SetActive(true);
            Texture2D tx = new Texture2D(2, 2);
            tx.LoadImage(Convert.FromBase64String(ss.imageCursor));
            Rect rt = new Rect(0, 0, tx.width, tx.height);
            sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

            cursor.rectTransform.sizeDelta = new Vector2(1 * LoadedSettings.cursorSizes.x, 1.2f * LoadedSettings.cursorSizes.y);
            cursor.sprite = sp;
        }
        else
        {
            ifSpriteIs = true;
            cursor.sprite = null;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            importCursor.gameObject.SetActive(false);
            customCursor.isOn = false;
        }
    }
    public void Escape()
    {
        gameObject.SetActive(false);
    }
    public void CustomCursor()
    {
        if (ifSpriteIs && customCursor.isOn != false)
        {
            Cursor.visible = true;
            var extensions = new[] {new ExtensionFilter("Image Files", "png", "jpg")};
            pathFile = StandaloneFileBrowser.OpenFilePanel("Виберіть зображення", "", extensions, false);
            try
            {
                string extension = Path.GetExtension(pathFile[0]).ToLower();

                if (extension != ".png" && extension != ".jpg")
                {
                    customCursor.isOn = false;
                }
                else if (pathFile.Length > 0 && File.Exists(pathFile[0]))
                {
                    Cursor.visible = false;
                    ss.extension = extension;
                    Texture2D tx = new Texture2D(2, 2);
                    tx.LoadImage(File.ReadAllBytes(pathFile[0]));
                    Rect rt = new Rect(0, 0, tx.width, tx.height);
                    sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

                    cursor.sprite = sp;
                    ifSpriteIs = false;

                    if (customCursor.isOn)
                    {
                        cursor.sprite = sp;
                        importCursor.gameObject.SetActive(true);
                    }
                    else
                    {
                        importCursor.gameObject.SetActive(false);
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                customCursor.isOn = false;
                Cursor.visible = true;
            }
        }
        else
        {
            if (customCursor.isOn)
            {
                cursor.sprite = sp;
                importCursor.gameObject.SetActive(true);
                Cursor.visible = false;
            }
            else
            {
                importCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
    }

    public void AddNewCustomCursor()
    {
        try
        {
            Cursor.visible = true;
            var extensions = new[] { new ExtensionFilter("Image Files", "png", "jpg") };
            pathFile = StandaloneFileBrowser.OpenFilePanel("Виберіть зображення", "", extensions, false);

            string extension = Path.GetExtension(pathFile[0]).ToLower();

            if (extension != ".png" && extension != ".jpg")
            {
                customCursor.isOn = false;
            }
            else if (pathFile.Length > 0 && File.Exists(pathFile[0]))
            {
                Cursor.visible = false;
                ss.extension = extension;
                Texture2D tx = new Texture2D(2, 2);
                tx.LoadImage(File.ReadAllBytes(pathFile[0]));
                Rect rt = new Rect(0, 0, tx.width, tx.height);
                sp = Sprite.Create(tx, rt, new Vector2(0.5f, 0.5f));

                cursor.sprite = sp;
            }
        }
        catch (IndexOutOfRangeException)
        {
            Cursor.visible = true;
        }
    }

    public void Binds()
    {
        setting.gameObject.SetActive(false);
        binds.gameObject.SetActive(true);
    }

    public void Mute()
    {
        if (!mute.isOn)
        {
            music.gameObject.SetActive(true);
            song.gameObject.SetActive(true);
            valueMusic = music.value;
            valueSong = song.value;
        }
        else
        {
            music.gameObject.SetActive(false);
            song.gameObject.SetActive(false);
            valueMusic = 0;
            valueSong = 0;
        }
        musicManager.ChangeVolume(valueMusic);
        soundManager.ChangeVolume(valueSong);

    }
    public void Music()
    {
        valueMusic = music.value;
        musicManager.ChangeVolume(valueMusic);
    }
    public void Song()
    {
        valueSong = song.value;
        soundManager.ChangeVolume(valueSong);
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
            stats.gameObject.SetActive(true);
        }
        else
        {
            inventory.gameObject.SetActive(false);
            quests.gameObject.SetActive(false);
            map.gameObject.SetActive(false);
            stats.gameObject.SetActive(false);
        }
        
    }
    public void ResetSettings()
    {
        ss.SetStandartSettings();
        LoadSaves();
        Save();
        musicManager.ChangeVolume(valueMusic);
        soundManager.ChangeVolume(valueSong);
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
        ss.mute = mute.isOn;
        ss.cursorSizes = cursor.rectTransform.sizeDelta;
        ss.sensivity = sensivity.value;
        if (!pause.isOn)
        {
            ss.inventoryPause = false;
            ss.questsPause = false;
            ss.mapPause = false;
            ss.statsPause = false;
            ss.inventoryPauseSaveDontUseHim = inventory.isOn;
            ss.questsPauseSaveDontUseHim = quests.isOn;
            ss.mapPauseSaveDontUseHim = map.isOn;
            ss.statsPauseSaveDontUseHim = stats.isOn;
            ss.pause = false;
        }
        else
        {
            ss.pause = true;
            ss.inventoryPause = inventory.isOn;
            ss.questsPause = quests.isOn;
            ss.mapPause = map.isOn;
            ss.statsPause = stats.isOn;
        }
        ss.customCursor = customCursor.isOn;

        string extension = ss.extension;
        cursor.gameObject.SetActive(true);
        if (extension == ".png")
            ss.imageCursor = Convert.ToBase64String(cursor.sprite.texture.EncodeToPNG());
        else if (extension == ".jpg")
            ss.imageCursor = Convert.ToBase64String(cursor.sprite.texture.EncodeToJPG());
        cursor.gameObject.SetActive(false);

        string json = JsonUtility.ToJson(ss);
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(JsonUtility.ToJson(ss));
        }
        LoadedSettings.LoadSettings(ss);
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
    public bool statsPause;
    public bool inventoryPauseSaveDontUseHim;
    public bool questsPauseSaveDontUseHim;
    public bool mapPauseSaveDontUseHim;
    public bool statsPauseSaveDontUseHim;
    public bool customCursor;
    public string imageCursor;
    public string extension;
    public bool mute;
    public bool pause;

    public void SetStandartSettings()
    {
        song = 50f;
        music = 50f;
        songSaveDontUseHim = 50f;
        musicSaveDontUseHim = 50f;
        cursorSizes = new Vector2(50,60);
        sensivity = 50f;
        inventoryPause = false;
        questsPause = false;
        mapPause = false;
        statsPause = false;
        inventoryPauseSaveDontUseHim = false;
        questsPauseSaveDontUseHim = false;
        mapPauseSaveDontUseHim=false;
        statsPauseSaveDontUseHim = false;
        customCursor = false;
        imageCursor = "";
        extension = "";
        mute = false;
        pause = false;

    }
} 
