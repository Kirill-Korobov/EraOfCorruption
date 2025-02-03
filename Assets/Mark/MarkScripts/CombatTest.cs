using UnityEngine;

public class CombatTest : MonoBehaviour
{
    public DamageType damageType;
    public SubDamageType subDamageType;
    public DefenseType defenseType;
    public SubDefenseType subDefenseType;
    public float damageAmount = 100f;
    public float baseDefensePercentage = 10f;
    public float subDefensePercentage = 10f;  
    void Start()
    {
        float finalDamage = CalculateFinalDamage(damageType, subDamageType, damageAmount, defenseType, subDefenseType);
        Debug.Log($"Damage: {damageAmount} ({damageType}, {subDamageType}), " +
                  $"Defense: {defenseType}, {subDefenseType}, Final Damage: {finalDamage}");
    }
    float CalculateFinalDamage(DamageType damageType, SubDamageType subDamageType, float damageAmount,
                               DefenseType defenseType, SubDefenseType subDefenseType)
    {
        float damageAfterBaseDefense = damageAmount;
        if ((DamageType)defenseType == damageType)
        {
            damageAfterBaseDefense = damageAmount * (1 - baseDefensePercentage / 100f);
        }
        float finalDamage = damageAfterBaseDefense;

        if ((SubDamageType)subDefenseType == subDamageType)
        {
            finalDamage = damageAfterBaseDefense * (1 - subDefensePercentage / 100f);
        }
        return finalDamage;
    }
}
public enum DamageType
{
    Physical,
    Magic,
    Absolute
}
public enum DefenseType
{
    Physical,
    Magic,
    Absolute
}
public enum SubDamageType
{
    Slashing,   
    Projectile, 
    Destructive,  

    Fire,  
    Frost, 
    Electric, 
    Poison, 

    Light,  
    Dark,   
}
public enum SubDefenseType
{
    Slashing,  
    Projectile, 
    Destructive,  

    Fire,  
    Frost, 
    Electric, 
    Poison, 

    Light,
    Dark,  
}
