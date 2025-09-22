using UnityEngine;

public class MapMenuOperator : MonoBehaviour
{
    [SerializeField] private PauseManager pauseManager;

    public void CloseMapMenu()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseManager.SetGameNotPaused();
        // StaticEffects.coroutines.gameObject.SetActive(true);
    }
}