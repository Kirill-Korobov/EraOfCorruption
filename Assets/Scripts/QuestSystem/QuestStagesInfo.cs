using UnityEngine;

public enum QuestStages
{
    cantStart,
    canStart,
    inProgress,
    canFinish,
    isFinished
}

public class QuestStagesInfo : MonoBehaviour
{
    [SerializeField] private QuestsInfo questsInfo;
    [HideInInspector] public QuestStages[] questStages;

    private void Awake()
    {
        questStages = new QuestStages[questsInfo._QuestInfo.Length];
        questStages[0] = QuestStages.canStart;
        questStages[1] = QuestStages.inProgress;
        questStages[2] = QuestStages.canFinish;
        // Initialize.
    }
}