using UnityEngine;

public class MainGameUIOperator : MonoBehaviour
{
    public Canvas mainCanvas;
    [SerializeField] private Canvas deathCanvas, pauseCanvas, inventoryCanvas, statisticsCanvas, mapCanvas, questCanvas, _NPCQuestCanvas, settingsCanvas, achievementsCanvas;

    private void Awake()
    {
        mainCanvas.gameObject.SetActive(true);
        SetAllCanvasesInactive();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!deathCanvas.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (settingsCanvas.gameObject.activeSelf || achievementsCanvas.gameObject.activeSelf)
                {
                    settingsCanvas.gameObject.SetActive(false);
                    achievementsCanvas.gameObject.SetActive(false);
                }
                else if (pauseCanvas.gameObject.activeSelf || inventoryCanvas.gameObject.activeSelf || statisticsCanvas.gameObject.activeSelf || mapCanvas.gameObject.activeSelf || questCanvas.gameObject.activeSelf)
                {
                    if (inventoryCanvas.gameObject.activeSelf)
                    {
                        StaticDropTake.sl.Exit();
                    }
                    SetAllCanvasesInactive();
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    StaticEffects.coroutines.gameObject.SetActive(true);
                    LoadedSettings.ifAnyOpen = false;
                }
                else if (!_NPCQuestCanvas.gameObject.activeSelf)
                {
                    pauseCanvas.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    StaticEffects.Save();
                    LoadedSettings.ifAnyOpen = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!inventoryCanvas.gameObject.activeSelf)
                {
                    SetAllCanvasesInactive();
                    inventoryCanvas.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (LoadedSettings.inventoryPause)
                    {
                        LoadedSettings.ifInventoryOpen = true;
                        StaticEffects.Save();
                    }
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    StaticDropTake.sl.Exit();
                    inventoryCanvas.gameObject.SetActive(false);
                    StaticEffects.coroutines.gameObject.SetActive(true);
                    LoadedSettings.ifInventoryOpen = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!mapCanvas.gameObject.activeSelf)
                {
                    SetAllCanvasesInactive();
                    mapCanvas.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (LoadedSettings.mapPause)
                    {
                        LoadedSettings.ifMapOpen = true;
                        StaticEffects.Save();
                    }
                }
                else
                {
                    mapCanvas.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    StaticEffects.coroutines.gameObject.SetActive(true);
                    LoadedSettings.ifMapOpen = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (!statisticsCanvas.gameObject.activeSelf)
                {
                    SetAllCanvasesInactive();
                    statisticsCanvas.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (LoadedSettings.statsPause)
                    {
                        LoadedSettings.ifStatsOpen = true;
                        StaticEffects.Save();
                    }
                }
                else
                {
                    statisticsCanvas.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    StaticEffects.coroutines.gameObject.SetActive(true);
                    LoadedSettings.ifStatsOpen = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!questCanvas.gameObject.activeSelf)
                {
                    SetAllCanvasesInactive();
                    questCanvas.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (LoadedSettings.questsPause)
                    {
                        StaticEffects.Save();
                        LoadedSettings.ifQuestsOpen = true;
                    }
                }
                else
                {
                    questCanvas.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    StaticEffects.coroutines.gameObject.SetActive(true);
                    LoadedSettings.ifQuestsOpen = false;
                }
            }
        }
    }

    private void SetAllCanvasesInactive()
    {
        pauseCanvas.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(false);
        statisticsCanvas.gameObject.SetActive(false);
        mapCanvas.gameObject.SetActive(false);
        questCanvas.gameObject.SetActive(false);
    }
}