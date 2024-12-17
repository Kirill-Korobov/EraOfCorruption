using UnityEngine;

public class QuestTasks : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;

    public void UpdateTasks()
    {
        for (int i = 0; i < questStagesInfo.questStages.Length; i++)
        {
            if (questStagesInfo.questStages[i] == QuestStages.inProgress)
            {
                switch (i)
                {
                    /* Прописати логіку умови виконання кожного квеста по такій схемі:
                     * 
                     *  case -1:
                        if (something)
                        {
                            questStagesInfo.questStages[i] = QuestStages.canFinish;
                        }
                        break;
                     */
                    default:
                        Debug.Log("No tasks for this quest.");
                        questStagesInfo.questStages[i] = QuestStages.canFinish;
                        break;
                }
            }
        }
    }
}