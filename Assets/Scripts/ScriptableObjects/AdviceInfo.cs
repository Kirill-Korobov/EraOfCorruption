using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AdviceInfoConfig", menuName = "ScriptableObjects/AdviceInfo")]

public class AdviceInfo : ScriptableObject
{
    [SerializeField] private string[] piecesOfAdvice;
    public string[] PiecesOfAdvice => piecesOfAdvice;
}