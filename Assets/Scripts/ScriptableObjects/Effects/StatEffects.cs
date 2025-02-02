using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatEffectConfig", menuName = "ScriptableObjects/StatEffect")]
public class StatEffects : ScriptableObject
{
    //Poison
    [Header("Poison Effects")]
    [SerializeField] private int poisionDMG;
    public int PoisionDMG => poisionDMG;

    [SerializeField] private int poisionTime;
    public int PoisionTime => poisionTime;

    [SerializeField] private int poisionCD;
    public int PoisionCD => poisionCD;

    [SerializeField] private int poisionDIS;
    public int PoisionDIS => poisionDIS;

    //Weakness
    [Space(10)] 
    [Header("Weakness Effects")]
    [SerializeField] private int weaknessTime;
    public int WeaknessTime => weaknessTime;

    [SerializeField] private int weaknessRate;
    public int WeaknessRate => weaknessRate;

    //Slowness
    [Space(10)]
    [Header("Slowness Effects")]
    [SerializeField] private int slownessTime;
    public int SlownessTime => slownessTime;

    [SerializeField] private int slownessRate;
    public int SlownessRate => slownessRate;

    //Hunger
    [Space(10)]
    [Header("Hunger Effects")]
    [SerializeField] private int hungerTime;
    public int HungerTime => hungerTime;

    [SerializeField] private int hungerRate;
    public int HungerRate => hungerRate;

    [SerializeField] private int hungerCD;
    public int HungerCD => hungerCD;

    //Penetration
    [Space(10)]
    [Header("Penetration Effects")]
    [SerializeField] private int penetrationTime;
    public int PenetrationTime => penetrationTime;

    [SerializeField] private int penetrationRate;
    public int PenetrationRate => penetrationRate;

    //Burn
    [Space(10)]
    [Header("Burn Effects")]
    [SerializeField] private int[] burnDMG;
    public int[] BurnDMG => burnDMG;

    [SerializeField] private int burnTime;
    public int BurnTime => burnTime;

    [SerializeField] private int burnCD;
    public int BurnCD => burnCD;

    //Blindness
    [Space(10)]
    [Header("Blindness Effects")]
    [SerializeField] private int blindnessTime;
    public int BlindnessTime => blindnessTime;

    [SerializeField] private int blindnessRate;
    public int BlindnessRate => blindnessRate;

    //Cursed
    [Space(10)]
    [Header("Cursed Effects")]
    [SerializeField] private int cursedTime;
    public int CursedTime => cursedTime;

    //Hex
    [Space(10)]
    [Header("Hex Effects")]
    [SerializeField] private int hexTime;
    public int HexTime => hexTime;

    [SerializeField] private float hexRate;
    public float HexRate => hexRate;

    //Regeneration
    [Header("Regeneration Effects")]
    [SerializeField] private int regeneration;
    public int Regeneration => regeneration;

    [SerializeField] private int regenerationTime;
    public int RegenerationTime => regenerationTime;

    [SerializeField] private int regenerationCD;
    public int RegenerationCD => regenerationCD;

    //Strength
    [Space(10)]
    [Header("Strength Effects")]
    [SerializeField] private int strengthTime;
    public int StrengthTime => strengthTime;

    [SerializeField] private int strengthRate;
    public int StrengthRate => strengthRate;

    //Speed
    [Space(10)]
    [Header("Speed Effects")]
    [SerializeField] private int speedTime;
    public int SpeedTime => speedTime;

    [SerializeField] private int speedRate;
    public int SpeedRate => speedRate;

    //VampirismHP
    [Space(10)]
    [Header("VampirismHP Effects")]
    [SerializeField] private int vampirismHP;
    public int VampirismHP => vampirismHP;

    [SerializeField] private int vampirismHPTime;
    public int VampirismHPTime => vampirismHPTime;

    //VampirismMana
    [Space(10)]
    [Header("VampirismHP Effects")]
    [SerializeField] private int vampirismMana;
    public int VampirismMana => vampirismMana;

    [SerializeField] private int vampirismManaTime;
    public int VampirismManaTime => vampirismManaTime;

    //Resistance
    [Space(10)]
    [Header("Resistance Effects")]
    [SerializeField] private int resistanceRate;
    public int ResistanceRate => resistanceRate;

    [SerializeField] private int resistanceTime;
    public int ResistanceTime => resistanceTime;

    //Shocks
    [Space(10)]
    [Header("Shocks Effects")]
    [SerializeField] private int shocksTime;
    public int ShocksTime => shocksTime;
}
