using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuestMenuOperator : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;
    [SerializeField] private QuestsInfo questsInfo;
    [SerializeField] private NPCsInfo _NPCsInfo;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private GameObject _NPCQuestPrefab, content, requirementsNotMetText;
    [SerializeField] private VerticalLayoutGroup contentVerticalLayoutGroup;
    [SerializeField] private QuestRewards questRewards;
    private Animator requirementsNotMetTextAnimator;
    private GameObject[] bufferQuests;
    private RectTransform contentRectTransform;
    private int interactingNPCID, questNumber, questIndex;

    private void Awake()
    {
        interactingNPCID = -1;
        contentRectTransform = content.GetComponent<RectTransform>();
        requirementsNotMetTextAnimator = requirementsNotMetText.GetComponent<Animator>();
    }

    public void SpawnQuests(int _interactingNPCID)
    {
        interactingNPCID = _interactingNPCID;
        questNumber = 0;
        for (int i = 0; i < _NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length; i++)
        {
            if (questStagesInfo.questStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] != QuestStages.isFinished)
            {
                questNumber++;
            }
        }
        titleText.text = $"{_NPCsInfo._NPCsInfo[interactingNPCID].name}'s quests";
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, questNumber * (_NPCQuestPrefab.GetComponent<RectTransform>().sizeDelta.y + contentVerticalLayoutGroup.spacing) - contentVerticalLayoutGroup.spacing);
        questIndex = 0;
        bufferQuests = new GameObject[questNumber];
        for (int i = 0; i < _NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length; i++)
        {
            if (questStagesInfo.questStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] != QuestStages.isFinished)
            {
                bufferQuests[questIndex] = Instantiate(_NPCQuestPrefab, contentRectTransform);
                bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[0].text = questsInfo._QuestInfo[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]].name;
                bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[1].text = $"Requirements: {questsInfo._QuestInfo[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]].requirementDescription}.";
                bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[2].text = $"Task: {questsInfo._QuestInfo[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]].taskDescription}.";
                bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[3].text = $"Reward: {questsInfo._QuestInfo[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]].rewardDescription}.";
                if (questsInfo._QuestInfo[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]].reusability)
                {
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[4].text = "Reusable: Yes";
                }
                else
                {
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[4].text = "Reusable: No";
                }
                int bufferNumber = _NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i];
                if (questStagesInfo.questStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.cantStart || questStagesInfo.questStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.canStart)
                {

                    bufferQuests[questIndex].GetComponentInChildren<Button>().onClick.AddListener(() => StartQuestButton(bufferNumber));
                    bufferQuests[questIndex].GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Start";
                }
                else if (questStagesInfo.questStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.inProgress)
                {
                    bufferQuests[questIndex].GetComponentInChildren<Button>().onClick.AddListener(() => RefuseQuest(bufferNumber));
                    bufferQuests[questIndex].GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Refuse";
                }
                else if (questStagesInfo.questStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.canFinish)
                {
                    bufferQuests[questIndex].GetComponentInChildren<Button>().onClick.AddListener(() => FinishQuest(bufferNumber));
                    bufferQuests[questIndex].GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Finish";
                }
                questIndex++;
            }
        }
    }

    public void DeleteQuests()
    {
        if (bufferQuests != null)
        {
            for (int i = 0; i < bufferQuests.Length; i++)
            {
                Destroy(bufferQuests[i]);
            }
        }
    }

    private void StartQuestButton(int _questIndex)
    {
        if (questStagesInfo.questStages[_questIndex] == QuestStages.canStart)
        {
            StartQuest(_questIndex);
        }
        else if (questStagesInfo.questStages[_questIndex] == QuestStages.cantStart)
        {
            requirementsNotMetText.gameObject.SetActive(true);
            requirementsNotMetTextAnimator.Play("RequirementsNotMetTextAnimation", 0, 0);
        }
    }

    public void DeactivateRequirementsNotMetText()
    {
        requirementsNotMetText.gameObject.SetActive(false);
    }

    private void StartQuest(int _questIndex)
    {
        questStagesInfo.questStages[_questIndex] = QuestStages.inProgress;
        DeleteQuests();
        SpawnQuests(interactingNPCID);
    }

    private void RefuseQuest(int _questIndex)
    {
        questStagesInfo.questStages[_questIndex] = QuestStages.cantStart;
        DeleteQuests();
        SpawnQuests(interactingNPCID);
    }

    private void FinishQuest(int _questIndex)
    {
        questRewards.GetReward(_questIndex);
        if (questsInfo._QuestInfo[_questIndex].reusability)
        {
            questStagesInfo.questStages[_questIndex] = QuestStages.cantStart;
        }
        else
        {
            questStagesInfo.questStages[_questIndex] = QuestStages.isFinished;
        }
        DeleteQuests();
        SpawnQuests(interactingNPCID);
    }
}