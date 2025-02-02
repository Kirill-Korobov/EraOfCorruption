using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TradesInfoConfig", menuName = "ScriptableObjects/TradesInfo")]

public class TradesInfo : ScriptableObject
{
    [SerializeField] private TradeInfo[] tradeInfo;
    public TradeInfo[] _TradeInfo => tradeInfo;
}

[Serializable]
public class TradeInfo
{
    public Product[] price;
    public string priceDescription;
    public Product[] goods;
    public string goodsDescription;
}

[Serializable]
public class Product
{
    public int productIndex;
    public int productQuantity;
}