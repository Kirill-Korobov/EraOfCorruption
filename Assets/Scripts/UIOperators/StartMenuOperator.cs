using UnityEngine;

public class StartMenuOperator : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas, achievementsCanvas, developersCanvas;
    [SerializeField] private GameObject startMenuContent, chooseGameWindow;

    private void Awake()
    {
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
        settingsCanvas.gameObject.SetActive(true);
    }

    public void OpenAchievements()
    {
        achievementsCanvas.gameObject.SetActive(true);
    }

    public void OpenDevelopers()
    {
        developersCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}