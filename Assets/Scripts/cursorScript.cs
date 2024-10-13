using UnityEngine;

public class cursorScript : MonoBehaviour
{
    private float idleTime = 5.0f;
    private float lastMouseMoveTime;
    private Vector3 lastMousePosition;
    public bool isCursorVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        lastMousePosition = Input.mousePosition;
        lastMouseMoveTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition != lastMousePosition)
        {
            lastMouseMoveTime = Time.time;
            lastMousePosition = Input.mousePosition;

            if (!isCursorVisible)
            {
                ShowCursor();
            }
        }

        if (Time.time - lastMouseMoveTime > idleTime)
        {
            if (isCursorVisible)
            {
                HideCursor();
            }
        }
    }

    void ShowCursor()
    {
        Cursor.visible = true;
        isCursorVisible = true;
    }

    void HideCursor()
    {
        Debug.Log("Cursor hide");
        Cursor.visible = false;
        isCursorVisible = false;
    }
}
