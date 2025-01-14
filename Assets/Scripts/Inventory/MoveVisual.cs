using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveVisual : MonoBehaviour
{
    Image image;   
    void Update()
    {
        Vector2 cursorPosition = Input.mousePosition; 
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(image.rectTransform.parent as RectTransform, cursorPosition, null, out anchoredPosition); 
        image.rectTransform.anchoredPosition = anchoredPosition;
    }
    private void Start()
    {
        image = GetComponent<Image>();
    }
}
