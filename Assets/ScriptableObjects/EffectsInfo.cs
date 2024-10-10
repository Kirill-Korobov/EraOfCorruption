using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectsInfoConfig", menuName = "ScriptableObjects/EffectsInfo")]

public class EffectsInfo : ScriptableObject
{
    [SerializeField] private Effect[] _effectsInfo;
    public Effect[] _EffectsInfo => _effectsInfo;
}