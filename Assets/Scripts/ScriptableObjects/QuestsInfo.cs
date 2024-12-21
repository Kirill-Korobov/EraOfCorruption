using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuestsInfoConfig", menuName = "ScriptableObjects/QuestsInfo")]

public class QuestsInfo : ScriptableObject
{
    [SerializeField] private QuestInfo[] questInfo;
    public QuestInfo[] _QuestInfo => questInfo;
}

[Serializable]
public class QuestInfo
{
    public string name;
    public string requirementDescription;
    public string taskDescription;
    public string rewardDescription;
    public bool reusability;
}