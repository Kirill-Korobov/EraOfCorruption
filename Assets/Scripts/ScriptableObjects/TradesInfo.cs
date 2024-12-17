using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TradesInfoConfig", menuName = "ScriptableObjects/TradesInfo")]

public class TradesInfo : ScriptableObject
{
    [SerializeField] private TradeInfo[] tradeInfo;
    public TradeInfo[] _TradeInfo => tradeInfo;
}

[Serializable]
public class TradeInfo
{
    public string priceDescription;
    public string productDescription;
}