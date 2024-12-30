using UnityEngine;

[CreateAssetMenu(fileName = "StatisticsInfoConfig", menuName = "ScriptableObjects/StatisticsInfo")]

public class StatisticsInfo : ScriptableObject
{
    // Level
    [Header("Level")]

    [SerializeField] private int maxLevel;
    public int MaxLevel => maxLevel;

    [SerializeField] private int statisticPointsPerLevel;
    public int StatisticPointsPerLevel => statisticPointsPerLevel;

    [SerializeField] private int maxStatisticLevelMultiplier;
    public int MaxStatisticLevelMultiplier => maxStatisticLevelMultiplier;

    // XP
    [Header("XP")]

    [SerializeField] private int[] nessaseryXPValuesToLevelUp;
    public int[] NessaseryXPValuesToLevelUp => nessaseryXPValuesToLevelUp;

    [SerializeField] private float[] _XPMultiplierValues;
    public float[] XPMultiplierValues => _XPMultiplierValues;

    // Health
    [Header("Health")]

    [SerializeField] private float[] maxHPValues;
    public float[] MaxHPValues => maxHPValues;

    [SerializeField] private float healthReplenishmentMultiplier;
    public float HealthReplenishmentMultiplier => healthReplenishmentMultiplier;

    [SerializeField] private float timeUntilHealthCanBeReplenished;
    public float TimeUntilHealthCanBeReplenished => timeUntilHealthCanBeReplenished;

    // Energy
    [Header("Energy")]

    [SerializeField] private float[] maxEnergyValues;
    public float[] MaxEnergyValues => maxEnergyValues;

    [SerializeField] private float energyReplenishmentMultiplier;
    public float EnergyReplenishmentMultiplier => energyReplenishmentMultiplier;

    // Mana
    [Header("Mana")]

    [SerializeField] private float manaReplenishmentMultiplier;
    public float ManaReplenishmentMultiplier => manaReplenishmentMultiplier;

    // Satiety
    [Header("Satiety")]

    [SerializeField] private float satietyMaxValue;
    public float SatietyMaxValue => satietyMaxValue;

    [SerializeField] private float satietySpendingMultiplier;
    public float SatietySpendingMultiplier => satietySpendingMultiplier;

    // Movement
    [Header("Movement")]

    // Walking

    [SerializeField] private float walkingSpeedValue;
    public float WalkingSpeedValue => walkingSpeedValue;

    // Running

    [SerializeField] private float[] runningSpeedMultiplierValues;
    public float[] RunningSpeedMultiplierValues => runningSpeedMultiplierValues;

    [SerializeField] private float runningEnergySpendingMultiplier;
    public float RunningEnergySpendingMultiplier => runningEnergySpendingMultiplier;

    // Jumping

    [SerializeField] private float jumpHeight;
    public float JumpHeight => jumpHeight;

    [SerializeField] private float jumpDuration;
    public float JumpDuration => jumpDuration;

    [SerializeField] private float timeBeforeFallingDuringJump;
    public float TimeBeforeFallingDuringJump => timeBeforeFallingDuringJump;

    [SerializeField] private AnimationCurve jumpCurve;
    public AnimationCurve JumpCurve => jumpCurve;

    [SerializeField] private int[] additionalJumpNumberValues;
    public int[] AdditionalJumpNumberValues => additionalJumpNumberValues;

    [SerializeField] private float jumpingEnergySpendingMultiplier;
    public float JumpingEnergySpendingMultiplier => jumpingEnergySpendingMultiplier;

    [SerializeField] private float everyNextAdditionalJumpEnergySpendingMultiplier;
    public float EveryNextAdditionalJumpEnergySpendingMultiplier => everyNextAdditionalJumpEnergySpendingMultiplier;

    // Falling

    [SerializeField] private float gravityValue;
    public float GravityValue => gravityValue;

    // Dashing

    [SerializeField] private int dashUnlockLevel;
    public int DashUnlockLevel => dashUnlockLevel;

    [SerializeField] private float dashDurationValue;
    public float DashingDurationValue => dashDurationValue;

    [SerializeField] private float[] dashingSpeedMultiplierValues;
    public float[] DashingSpeedMultiplierValues => dashingSpeedMultiplierValues;

    [SerializeField] private float[] dashingRechargeTimeValues;
    public float[] DashingRechargeTimeValues => dashingRechargeTimeValues;

    [SerializeField] private float dashingEnergySpendingMultiplier;
    public float DashingEnergySpendingMultiplier => dashingEnergySpendingMultiplier;

    // Teleportation

    [SerializeField] private int teleportationUnlockLevel;
    public int TeleportationUnlockLevel => teleportationUnlockLevel;

    [SerializeField] private float[] teleportationMaxDistanceValues;
    public float[] TeleportationMaxDistanceValues => teleportationMaxDistanceValues;

    [SerializeField] private float[] teleportationRechargeTimeValues;
    public float[] TeleportationRechargeTimeValues => teleportationRechargeTimeValues;

    [SerializeField] private float teleportationEnergySpendingMultiplier;
    public float TeleportationEnergySpendingMultiplier => teleportationEnergySpendingMultiplier;

    // Close combat
    [Header("Close combat")]

    [SerializeField] private float[] closeCombatStatsMultiplierValues;
    public float[] CloseCombatStatsMultiplierValues => closeCombatStatsMultiplierValues;

    [SerializeField] private float[] closeCombatAdditionalManaValues;
    public float[] ÑloseCombatAdditionalManaValues => closeCombatAdditionalManaValues;

    // Ranged combat
    [Header("Ranged combat")]

    [SerializeField] private float[] rangedCombatStatsMultiplierValues;
    public float[] RangedCombatStatsMultiplierValues => rangedCombatStatsMultiplierValues;

    [SerializeField] private float[] rangedCombatAdditionalManaValues;
    public float[] RangedCombatAdditionalManaValues => rangedCombatAdditionalManaValues;

    // Magic combat
    [Header("Magic combat")]

    [SerializeField] private float[] magicCombatStatsMultiplierValues;
    public float[] MagicCombatStatsMultiplierValues => magicCombatStatsMultiplierValues;

    [SerializeField] private float[] magicCombatAdditionalManaValues;
    public float[] MagicCombatAdditionalManaValues => magicCombatAdditionalManaValues;

    // NPCs
    [Header("NPCs")]

    [SerializeField] private float _NPCMaxInteractionDistance;
    public float NPCMaxInteractionDistance => _NPCMaxInteractionDistance;
}