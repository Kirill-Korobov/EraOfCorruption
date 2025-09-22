using UnityEngine;

[CreateAssetMenu(fileName = "QuestRewardVariableValuesInfoConfig", menuName = "ScriptableObjects/QuestRewardVariableValuesInfo")]

public class QuestRewardVariableValuesInfo : ScriptableObject
{
    [SerializeField] private int motherGiftAppleNumber;
    public int MotherGiftAppleNumber => motherGiftAppleNumber;

    [SerializeField] private int fatherGiftwoodenArrowsNumber;
    public int FatherGiftwoodenArrowsNumber => fatherGiftwoodenArrowsNumber;

    [SerializeField] private int bestFriendGiftMoneyAmount;
    public int BestFriendGiftMoneyAmount => bestFriendGiftMoneyAmount;

    [SerializeField] private int banditKillerMoneyAmount;
    public int BanditKillerMoneyAmount => banditKillerMoneyAmount;

    [SerializeField] private int heroOfTheForestMoneyAmount;
    public int HeroOfTheForestMoneyAmount => heroOfTheForestMoneyAmount;

    [SerializeField] private int sporeWarMoneyAmount;
    public int SporeWarMoneyAmount => sporeWarMoneyAmount;

    [SerializeField] private int goblinTroubleMoneyAmount;
    public int GoblinTroubleMoneyAmount => goblinTroubleMoneyAmount;
}