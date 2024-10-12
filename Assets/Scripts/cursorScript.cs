using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 cursorPosition = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, cursorPosition, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
