using UnityEngine;

public class QuestRequirements : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;
    [SerializeField] private QuestRequirementVariableValuesInfo questRequirementVariableValuesInfo;
    [SerializeField] private MC_LevelManager levelManager;

    public void UpdateRequirements()
    {
        int bufferLevel = levelManager.Level + 1;
        for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
        {
            if (questStagesInfo._QuestStages[i] == QuestStages.cantStart || questStagesInfo._QuestStages[i] == QuestStages.canStart)
            {
                switch (i)
                {
                    case 5:
                        if (bufferLevel >= questRequirementVariableValuesInfo.BanditKillerRequiredLevel)
                        {
                            questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        }
                        break;
                    case 6:
                        if (bufferLevel >= questRequirementVariableValuesInfo.HeroOfTheForestRequiredLevel)
                        {
                            questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        }
                        break;
                    case 7:
                        if (bufferLevel >= questRequirementVariableValuesInfo.SporeWarRequiredLevel)
                        {
                            questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        }
                        break;
                    case 8:
                        if (bufferLevel >= questRequirementVariableValuesInfo.GoblinTroubleRequiredLevel)
                        {
                            questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        }
                        break;
                    default:
                        questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        break;
                }
            }
        }
    }
}