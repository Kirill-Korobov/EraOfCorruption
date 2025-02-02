using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuOperator : MonoBehaviour
{
    public void CloseInventoryMenu()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StaticEffects.coroutines.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        StaticDropTake.sl.Exit();
    }
}
