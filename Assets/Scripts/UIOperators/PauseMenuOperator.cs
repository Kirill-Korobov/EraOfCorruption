using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuOperator : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas, achievementsCanvas;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void BackToGameButton()
    {
        gameObject.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsCanvas.gameObject.SetActive(true);
    }

    public void AchievementsButton()
    {
        achievementsCanvas.gameObject.SetActive(true);
    }

    public void SaveAndExitButton()
    {
        // Save progress.
        SceneManager.LoadScene("StartMenu");
    }
}