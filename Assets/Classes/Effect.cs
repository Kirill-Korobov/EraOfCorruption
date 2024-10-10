using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Effect
{
    protected enum EffectType
    {
       
    }
    [SerializeField] protected EffectType effectType;
    [SerializeField] protected UnityEvent effectFunction;
}