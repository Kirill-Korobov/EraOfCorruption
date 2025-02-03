public class Defense
{
    public float defensePercentage; 
    public virtual float GetDefense()
    {
        return defensePercentage;
    }
}
public class PhysicalDefense : Defense
{
    public PhysicalDefenseSubType physicalDefenseType;
    public PhysicalDefense(float percentage, PhysicalDefenseSubType type)
    {
        defensePercentage = percentage;
        physicalDefenseType = type;
    }
    public enum PhysicalDefenseSubType
    {
        Slashing,   
        Piercing,   
        Destructive 
    }
}
public class MagicDefense : Defense
{
    public MagicDefenseSubType magicDefenseType;
    public MagicDefense(float percentage, MagicDefenseSubType type)
    {
        defensePercentage = percentage;
        magicDefenseType = type;
    }
    public enum MagicDefenseSubType
    {
        Fire,      
        Frost,     
        Lightning,  
        Poison     
    }
}
public class AbsoluteDefense : Defense
{
    public AbsoluteDefenseSubType absoluteDefenseType; 
    public AbsoluteDefense(float percentage, AbsoluteDefenseSubType type)
    {
        defensePercentage = percentage;
        absoluteDefenseType = type;
    }
    public enum AbsoluteDefenseSubType
    {
        Light,  
        Dark    
    }
}
