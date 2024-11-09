using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsInfoConfig", menuName = "ScriptableObjects/ItemsInfo")]

public class ItemsInfo : ScriptableObject
{
    [SerializeField] private Item[] _itemsInfo;
    public Item[] _ItemsInfo => _itemsInfo;
}