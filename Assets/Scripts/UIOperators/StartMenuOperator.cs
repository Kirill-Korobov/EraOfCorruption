using UnityEngine;

public class StartMenuOperator : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas, achievementsCanvas, developersCanvas;
    [SerializeField] private GameObject startMenuContent, chooseGameWindow;

    private void Awake()
    {
        startMenuContent.SetActive(true);
        chooseGameWindow.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        achievementsCanvas.gameObject.SetActive(false);
        developersCanvas.gameObject.SetActive(false);
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