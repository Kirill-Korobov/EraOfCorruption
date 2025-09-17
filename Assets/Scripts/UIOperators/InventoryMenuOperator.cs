using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuOperator : MonoBehaviour
{
    [SerializeField] private PauseManager pauseManager;

    public void CloseInventoryMenu()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseManager.SetGameNotPaused();
        // StaticEffects.coroutines.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        // StaticDropTake.sl.Exit();
    }
}