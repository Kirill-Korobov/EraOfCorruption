using UnityEngine;

public class QuestRequirements : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;

    public void UpdateRequirements()
    {
        for (int i = 0; i < questStagesInfo.questStages.Length; i++)
        {
            if (questStagesInfo.questStages[i] == QuestStages.cantStart || questStagesInfo.questStages[i] == QuestStages.canStart)
            {
                switch (i)
                {
                    /* Прописати логіку умови початку кожного квеста по такій схемі:
                     *  
                     *  case -1:
                        if (something)
                        {
                            questStagesInfo.questStages[i] = QuestStages.canStart;
                        }
                        else
                        {
                            questStagesInfo.questStages[i] = QuestStages.cantStart;
                        }
                        break;
                     */
                    default:
                        Debug.Log("No requirements for this quest.");
                        questStagesInfo.questStages[i] = QuestStages.canStart;
                        break;
                }
            }
        }
    }
}