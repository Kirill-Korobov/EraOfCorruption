using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XPInfoConfig", menuName = "ScriptableObjects/XPInfo")]

public class XPInfo : ScriptableObject
{
    [SerializeField] private int[] nessaseryXP;
    public int[] NessaseryXP => nessaseryXP;
}