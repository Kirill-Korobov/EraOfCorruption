using UnityEngine;

public class MapMenuOperator : MonoBehaviour
{
    public void CloseMapMenu()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}