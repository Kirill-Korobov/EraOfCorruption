using UnityEngine;

[CreateAssetMenu(fileName = "QuestTaskVariableValuesInfoConfig", menuName = "ScriptableObjects/QuestTaskVariableValuesInfo")]

public class QuestTaskVariableValuesInfo : ScriptableObject
{
    [SerializeField] private int banditKillerIndex;
    public int BanditKillerIndex => banditKillerIndex;

    [SerializeField] private int banditKillerBanditNumber;
    public int BanditKillerBanditNumber => banditKillerBanditNumber;

    [SerializeField] private int heroOfTheForestIndex;
    public int HeroOfTheForestIndex => heroOfTheForestIndex;

    [SerializeField] private int sporeWarIndex;
    public int SporeWarIndex => sporeWarIndex;

    [SerializeField] private int sporeWarMushroomNumber;
    public int SporeWarMushroomNumber => sporeWarMushroomNumber;

    [SerializeField] private int goblinTroubleIndex;
    public int GoblinTroubleIndex => goblinTroubleIndex;

    [SerializeField] private int goblinTroubleGoblinNumber;
    public int GoblinTroubleGoblinNumber => goblinTroubleGoblinNumber;
}