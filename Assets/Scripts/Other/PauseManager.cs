using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [HideInInspector] public bool pause;
    [SerializeField] private Animator mainCharacterAnimator;

    private void Awake()
    {
        SetGameNotPaused();
    }

    public void SetGamePaused()
    {
        if (mainCharacterAnimator != null && mainCharacterAnimator.isActiveAndEnabled)
        {
            mainCharacterAnimator.speed = 0f;
        }
        Time.timeScale = 0f;
        pause = true;
    }

    public void SetGameNotPaused()
    {
        if (mainCharacterAnimator != null && mainCharacterAnimator.isActiveAndEnabled)
        {
            mainCharacterAnimator.speed = 1f;
        }
        Time.timeScale = 1f;
        pause = false;
    }
}