using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class EffectsPosition
{
    private static Vector2[] positions = 
        {
            new Vector2(-876, 172), 
            new Vector2(-816, 172), 
            new Vector2(-756, 172), 
            new Vector2(-696, 172), 
            new Vector2(-636, 172), 
            new Vector2(-576, 172), 
            new Vector2(-516, 172),
            new Vector2(-876, 110),
            new Vector2(-816, 110),
            new Vector2(-756, 110),
            new Vector2(-696, 110),
            new Vector2(-636, 110),
            new Vector2(-576, 110),
            new Vector2(-516, 110),
            new Vector2(-876, 48), 
            new Vector2(-816, 48), 
    };
    private static List<Image> images = new List<Image>();

    public static void SetImageAsync(Image image)
    {
        if (!images.Contains(image))
        {
            images.Add(image);
            SetImage();
        }
    }

    public static void DeleteImageAsync(Image image)
    { 
        int index = images.IndexOf(image);

        if (index >= 0)
        {
            images.RemoveAt(index);
            SetImage();
        }
        
    }
    private static void SetImage()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].rectTransform.anchoredPosition = positions[i];
        }
    }



}
