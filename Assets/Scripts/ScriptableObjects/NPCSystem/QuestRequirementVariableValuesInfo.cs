using UnityEngine;

[CreateAssetMenu(fileName = "QuestRequirementVariableValuesInfoConfig", menuName = "ScriptableObjects/QuestRequirementVariableValuesInfo")]

public class QuestRequirementVariableValuesInfo : ScriptableObject
{
    [SerializeField] private int banditKillerRequiredLevel;
    public int BanditKillerRequiredLevel => banditKillerRequiredLevel;

    [SerializeField] private int heroOfTheForestRequiredLevel;
    public int HeroOfTheForestRequiredLevel => heroOfTheForestRequiredLevel;

    [SerializeField] private int sporeWarRequiredLevel;
    public int SporeWarRequiredLevel => sporeWarRequiredLevel;

    [SerializeField] private int goblinTroubleRequiredLevel;
    public int GoblinTroubleRequiredLevel => goblinTroubleRequiredLevel;
}