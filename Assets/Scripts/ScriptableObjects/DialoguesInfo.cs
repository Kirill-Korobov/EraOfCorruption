using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialoguesInfoConfig", menuName = "ScriptableObjects/DialoguesInfo")]

public class DialoguesInfo : ScriptableObject
{
    [SerializeField] private DialogueInfo[] dialoguesInfo;
    public DialogueInfo[] _DialoguesInfo => dialoguesInfo;
}

[Serializable]
public class DialogueInfo
{
    public Replica[] dialogue;
}

public enum Speaker
{
    MainCharacter,
    NPC
}

[Serializable]
public class Replica
{
    public string replicaText;
    public Speaker speaker;
}