using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyColorsInfoConfig", menuName = "ScriptableObjects/DifficultyColorsInfo")]

public class DifficultyColorsInfo : ScriptableObject
{
    [SerializeField] private Color easyDifficultyColor;
    public Color EasyDifficultyColor => easyDifficultyColor;

    [SerializeField] private Color mediumDifficultyColor;
    public Color MediumDifficultyColor => mediumDifficultyColor;

    [SerializeField] private Color hardDifficultyColor;
    public Color HardDifficultyColor => hardDifficultyColor;

    [SerializeField] private Color crazyDifficultyColor;
    public Color CrazyDifficultyColor => crazyDifficultyColor;
}