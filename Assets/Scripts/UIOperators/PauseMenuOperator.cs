using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuOperator : MonoBehaviour
{
    [SerializeField] private Canvas settingsCanvas, achievementsCanvas;
    [SerializeField] private PauseManager pauseManager;

    private void OnEnable()
    {
        pauseManager.SetGamePaused();
    }

    private void OnDisable()
    {
        pauseManager.SetGameNotPaused();
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