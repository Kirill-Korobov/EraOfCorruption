using UnityEngine;

public class StartMenuOperator : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas, achievementsCanvas, developersCanvas;
    [SerializeField] private GameObject startMenuContent, chooseGameWindow, createGameWindow, deleteGameWindow, bindWindow;

    private void Awake()
    {
        startMenuContent.SetActive(true);
        chooseGameWindow.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        achievementsCanvas.gameObject.SetActive(false);
        developersCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (createGameWindow.activeSelf || deleteGameWindow.activeSelf)
            {
                createGameWindow.SetActive(false);
                deleteGameWindow.SetActive(false);
                chooseGameWindow.SetActive(true);
            }
            else if (bindWindow.activeSelf)
            {
                bindWindow.SetActive(false);
            }
            else if (chooseGameWindow.activeSelf || settingsCanvas.gameObject.activeSelf || achievementsCanvas.gameObject.activeSelf)
            {
                startMenuContent.SetActive(true);
                chooseGameWindow.gameObject.SetActive(false);
                settingsCanvas.gameObject.SetActive(false);
                achievementsCanvas.gameObject.SetActive(false);
                developersCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void OpenGameChoice()
    {
        startMenuContent.SetActive(false);
        chooseGameWindow.gameObject.SetActive(true);
    }

    public void OpenSettings()
    {
        startMenuContent.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void OpenAchievements()
    {
        startMenuContent.SetActive(false);
        achievementsCanvas.gameObject.SetActive(true);
    }

    public void OpenDevelopers()
    {
        startMenuContent.SetActive(false);
        developersCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}