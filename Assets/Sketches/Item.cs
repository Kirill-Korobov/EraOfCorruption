using System;
using UnityEngine;

[Serializable]
public class Item
{
    protected enum ItemType
    {
        weapon,
        food,
        potion,
        helmet,
        questItem,
        breastplate,
        boots,
        accessory,
        pet,
        transport
    }
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected int maxNumberInOneSlot;
    [SerializeField] protected Sprite itemSprite;
    [SerializeField] protected GameObject itemPrefab;
}