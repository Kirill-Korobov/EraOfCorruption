using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatisticsInfoConfig", menuName = "ScriptableObjects/StatisticsInfo")]

public class StatisticsInfo : ScriptableObject
{
    [SerializeField] private int statisticPointsPerLevel;
    public int StatisticPointsPerLevel => statisticPointsPerLevel;

    [SerializeField] private int maxStatisticLevelMultiplier;
    public int MaxStatisticLevelMultiplier => maxStatisticLevelMultiplier;

    [SerializeField] private float[] _HPLevels;
    public float[] HPLevels => _HPLevels;

    [SerializeField] private float[] energyLevels;
    public float[] EnergyLevels => energyLevels;

    [SerializeField] private float[] movementSpeedLevels;
    public float[] MovementSpeedLevels => movementSpeedLevels;

    [SerializeField] private int[] movementJumpNumberLevels;
    public int[] MovementJumpNumberLevels => movementJumpNumberLevels;

    [SerializeField] private float[] _XPMultiplierLevels;
    public float[] XPMultiplierLevels => _XPMultiplierLevels;

    [SerializeField] private float[] closeCombatLevels;
    public float[] CloseCombatLevels => closeCombatLevels;

    [SerializeField] private float[] rangedCombatLevels;
    public float[] RangedCombatLevels => rangedCombatLevels;

    [SerializeField] private float[] magicCombatLevels;
    public float[] MagicCombatLevels => magicCombatLevels;
}