using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsLogic : MonoBehaviour
{
    private List<Image> images = new List<Image>();
    [SerializeField] private List<Image> slots = new List<Image>();

    public void AddItem(Image item)
    {
        images.Add(item);
        
        slots[images.IndexOf(item)] = item;
    }
}

public struct InventoryItems
{
    public Image image;
    public GameObject gameObject;

    public InventoryItems(Image image, GameObject gameObject)
    {
        this.image = image;
        this.gameObject = gameObject;
    }
}
