using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [HideInInspector] public bool pause;

    private void Awake()
    {
        SetGameNotPaused();
    }

    public void SetGamePaused()
    {
        Time.timeScale = 0f;
        pause = true;
    }

    public void SetGameNotPaused()
    {
        Time.timeScale = 1f;
        pause = false;
    }
}