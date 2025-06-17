using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeForMovingCursor : MonoBehaviour
{
    [SerializeField] private Image cursor;
    bool onenabe= false;
    private void Start()
    {
        if (!LoadedSettings.customCursor)
        {
            Cursor.visible = true;
            cursor.gameObject.SetActive(false);
        }
        else
        {
            cursor.sprite = LoadedSettings.imageCursor;
            cursor.rectTransform.sizeDelta = new Vector2(1 * LoadedSettings.cursorSizes.x, 1.2f * LoadedSettings.cursorSizes.y);
            Cursor.visible= false;
            cursor.gameObject.SetActive(true);
        }
        onenabe = true;
    }
    private void OnEnable()
    {
        if (onenabe)
        {
            if (!LoadedSettings.customCursor)
            {
                Cursor.visible = true;
                cursor.gameObject.SetActive(false);
            }
            else
            {
                cursor.sprite = LoadedSettings.imageCursor; 
                cursor.rectTransform.localScale = new Vector3(LoadedSettings.cursorSizes.x, LoadedSettings.cursorSizes.y, 1);
                Cursor.visible = false;
                cursor.gameObject.SetActive(true);
            }
        }
    }
    [Obsolete]
    private void Update()
    {
        if (cursor.gameObject.active == true)
        {
            cursor.transform.position = new Vector3(Input.mousePosition.x + (cursor.rectTransform.sizeDelta.x / 2), Input.mousePosition.y - (cursor.rectTransform.sizeDelta.y / 2));
        }

    }
    private void Awake()
    {
        
    }
}
