using UnityEngine;

[CreateAssetMenu(fileName = "AdviceInfoConfig", menuName = "ScriptableObjects/AdviceInfo")]

public class AdviceInfo : ScriptableObject
{
    [SerializeField] private string[] piecesOfAdvice;
    public string[] PiecesOfAdvice => piecesOfAdvice;
}