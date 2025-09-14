using UnityEngine;

public class QuestTasks : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;
    [SerializeField] private QuestTaskVariableValuesInfo questTaskVariableValuesInfo;
    [SerializeField] private GameStatsManager gameStatsManager;
    private GameStats currentGameStats;

    private void Start()
    {
        switch (GameStatsManager.currentGame)
        {
            case 1:
                currentGameStats = gameStatsManager.game1Stats;
                break;
            case 2:
                currentGameStats = gameStatsManager.game2Stats;
                break;
            case 3:
                currentGameStats = gameStatsManager.game3Stats;
                break;
            default:
                currentGameStats = gameStatsManager.game1Stats;
                break;
        }
    }

    public void UpdateTasks()
    {
        for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
        {
            if (questStagesInfo._QuestStages[i] == QuestStages.inProgress)
            {
                switch (i)
                {
                    case 5:
                        if (questStagesInfo._QuestStages[questTaskVariableValuesInfo.BanditKillerBanditNumber] != QuestStages.canFinish && currentGameStats.questVariableStats.banditKillerKilledBanditNumber >= questTaskVariableValuesInfo.BanditKillerBanditNumber)
                        {
                            questStagesInfo._QuestStages[questTaskVariableValuesInfo.BanditKillerBanditNumber] = QuestStages.canFinish;
                        }
                        break;
                    case 6:
                        if (questStagesInfo._QuestStages[questTaskVariableValuesInfo.HeroOfTheForestIndex] != QuestStages.canFinish && currentGameStats.questVariableStats.killedForestDragon)
                        {
                            questStagesInfo._QuestStages[questTaskVariableValuesInfo.HeroOfTheForestIndex] = QuestStages.canFinish;
                        }
                        break;
                    case 7:
                        if (questStagesInfo._QuestStages[questTaskVariableValuesInfo.SporeWarIndex] != QuestStages.canFinish && currentGameStats.questVariableStats.sporeWarKilledMushroomNumber >= questTaskVariableValuesInfo.SporeWarMushroomNumber)
                        {
                            questStagesInfo._QuestStages[questTaskVariableValuesInfo.SporeWarIndex] = QuestStages.canFinish;
                        }
                        break;
                    case 8:
                        if (questStagesInfo._QuestStages[questTaskVariableValuesInfo.GoblinTroubleIndex] != QuestStages.canFinish && currentGameStats.questVariableStats.goblinTroubleKilledGoblinNumber >= questTaskVariableValuesInfo.GoblinTroubleGoblinNumber)
                        {
                            questStagesInfo._QuestStages[questTaskVariableValuesInfo.GoblinTroubleIndex] = QuestStages.canFinish;
                        }
                        break;
                    default:
                        questStagesInfo._QuestStages[i] = QuestStages.canFinish;
                        break;
                }
            }
        }
    }
}