using UnityEngine;
public class Damage
{
    public float damageAmount;
    public virtual float GetDamage()
    {
        return damageAmount;
    }
}
public class PhysicalDamage : Damage
{
    public PhysicalSubType physicalType; 
    public PhysicalDamage(float amount, PhysicalSubType type)
    {
        damageAmount = amount;
        physicalType = type;
    }
    public enum PhysicalSubType
    {
        Slashing,  
        Piercing,   
        Destructive 
    }
}
public class MagicDamage : Damage
{
    public MagicSubType magicType; 

    public MagicDamage(float amount, MagicSubType type)
    {
        damageAmount = amount;
        magicType = type;
    }

    public enum MagicSubType
    {
        Fire,      
        Frost,    
        Lightning, 
        Poison    
    }
}
public class AbsoluteDamage : Damage
{
    public AbsoluteSubType absoluteType; 
    public AbsoluteDamage(float amount, AbsoluteSubType type)
    {
        damageAmount = amount;
        absoluteType = type;
    }
    public enum AbsoluteSubType
    {
        Light,
        Dark   
    }
}
