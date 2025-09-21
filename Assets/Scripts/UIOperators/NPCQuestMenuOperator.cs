using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuestMenuOperator : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;
    [SerializeField] private QuestRequirements questRequirements;
    [SerializeField] private QuestTasks questTasks;
    [SerializeField] private QuestsInfo questsInfo;
    [SerializeField] private NPCsInfo _NPCsInfo;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private GameObject _NPCQuestPrefab, content;
    [SerializeField] private VerticalLayoutGroup contentVerticalLayoutGroup;
    [SerializeField] private QuestRewards questRewards;
    [SerializeField] private RequirementsNotMetTextBehaviour requirementsNotMetTextBehaviour;
    [SerializeField] private float additionalSpace;
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private QuestTaskVariableValuesInfo questTaskVariableValuesInfo;
    private Coroutine showRequirementsNotMetTextCoroutine;
    private GameObject[] bufferQuests;
    private RectTransform contentRectTransform;
    private int interactingNPCID, questNumber, questIndex;
    private GameStats currentGameStats;

    private void Awake()
    {
        interactingNPCID = -1;
        contentRectTransform = content.GetComponent<RectTransform>();
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

    private void OnEnable()
    {
        requirementsNotMetTextBehaviour.MakeTransparent();
    }

    private void OnDisable()
    {
        DeleteQuests();
    }

    public void SpawnQuests(int _interactingNPCID)
    {
        interactingNPCID = _interactingNPCID;
        questRequirements.UpdateRequirements();
        questTasks.UpdateTasks();
        questNumber = 0;
        for (int i = 0; i < _NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length; i++)
        {
            if (questStagesInfo._QuestStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] != QuestStages.isFinished)
            {
                questNumber++;
            }
        }
        titleText.text = $"{_NPCsInfo._NPCsInfo[interactingNPCID].name}'s quests";
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, questNumber * (_NPCQuestPrefab.GetComponent<RectTransform>().sizeDelta.y + contentVerticalLayoutGroup.spacing) - contentVerticalLayoutGroup.spacing + additionalSpace);
        questIndex = 0;
        bufferQuests = new GameObject[questNumber];
        for (int i = 0; i < _NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length; i++)
        {
            if (questStagesInfo._QuestStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] != QuestStages.isFinished)
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
                if (questStagesInfo._QuestStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.cantStart || questStagesInfo._QuestStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.canStart)
                {
                    bufferQuests[questIndex].GetComponentInChildren<Button>().onClick.AddListener(() => StartQuestButton(bufferNumber));
                    bufferQuests[questIndex].GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Start";
                }
                else if (questStagesInfo._QuestStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.inProgress)
                {
                    bufferQuests[questIndex].GetComponentInChildren<Button>().onClick.AddListener(() => RefuseQuest(bufferNumber));
                    bufferQuests[questIndex].GetComponentInChildren<Button>().GetComponentInChildren<TMP_Text>().text = "Refuse";
                }
                else if (questStagesInfo._QuestStages[_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes[i]] == QuestStages.canFinish)
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
        if (questStagesInfo._QuestStages[_questIndex] == QuestStages.canStart)
        {
            StartQuest(_questIndex);
        }
        else if (questStagesInfo._QuestStages[_questIndex] == QuestStages.cantStart)
        {
            if (showRequirementsNotMetTextCoroutine != null)
            {
                StopCoroutine(showRequirementsNotMetTextCoroutine);
            }
            showRequirementsNotMetTextCoroutine = StartCoroutine(requirementsNotMetTextBehaviour.ShowRequirementsNotMetText());
        }
    }

    private void StartQuest(int _questIndex)
    {
        questStagesInfo._QuestStages[_questIndex] = QuestStages.inProgress;
        DeleteQuests();
        SpawnQuests(interactingNPCID);
    }

    private void RefuseQuest(int _questIndex)
    {
        questStagesInfo._QuestStages[_questIndex] = QuestStages.cantStart;
        if (_questIndex == questTaskVariableValuesInfo.BanditKillerIndex)
        {
            currentGameStats.questVariableStats.banditKillerKilledBanditNumber = 0;
        }
        else if (_questIndex == questTaskVariableValuesInfo.SporeWarIndex)
        {
            currentGameStats.questVariableStats.sporeWarKilledMushroomNumber = 0;
        }
        else if (_questIndex == questTaskVariableValuesInfo.GoblinTroubleIndex)
        {
            currentGameStats.questVariableStats.goblinTroubleKilledGoblinNumber = 0;
        }
        DeleteQuests();
        SpawnQuests(interactingNPCID);
    }

    private void FinishQuest(int _questIndex)
    {
        questRewards.GetReward(_questIndex);
        if (questsInfo._QuestInfo[_questIndex].reusability)
        {
            questStagesInfo._QuestStages[_questIndex] = QuestStages.cantStart;
        }
        else
        {
            questStagesInfo._QuestStages[_questIndex] = QuestStages.isFinished;
        }
        DeleteQuests();
        SpawnQuests(interactingNPCID);
    }
}