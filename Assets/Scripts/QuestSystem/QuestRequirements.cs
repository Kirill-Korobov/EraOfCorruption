using UnityEngine;

public class QuestRequirements : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;

    public void UpdateRequirements()
    {
        for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
        {
            if (questStagesInfo._QuestStages[i] == QuestStages.cantStart || questStagesInfo._QuestStages[i] == QuestStages.canStart)
            {
                switch (i)
                {
                    /* Прописати логіку умови початку кожного квеста по такій схемі:
                     *  
                     *  case -1:
                        if (something)
                        {
                            questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        }
                        else
                        {
                            questStagesInfo._QuestStages[i] = QuestStages.cantStart;
                        }
                        break;
                     */
                    default:
                        Debug.Log("No requirements for this quest.");
                        questStagesInfo._QuestStages[i] = QuestStages.canStart;
                        break;
                }
            }
        }
    }
}