using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCsInfoConfig", menuName = "ScriptableObjects/NPCsInfo")]

public class NPCsInfo : ScriptableObject
{
    [SerializeField] private NPCInfo[] __NPCsInfo;
    public NPCInfo[] _NPCsInfo => __NPCsInfo;
}

[Serializable]
public class NPCInfo
{
    public string name;
    public int dialogueIndex;
    public int[] questIndexes;
    public int[] tradesIndexes;
}