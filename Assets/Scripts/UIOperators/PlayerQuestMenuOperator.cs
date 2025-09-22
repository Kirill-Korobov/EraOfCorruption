using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestMenuOperator : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;
    [SerializeField] private QuestsInfo questsInfo;
    [SerializeField] private QuestTaskVariableValuesInfo questTaskVariableValuesInfo;
    [SerializeField] private QuestTasks questTasks;
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private GameObject playerQuestPrefab, content;
    [SerializeField] private TMP_Text noQuestsYetText;
    [SerializeField] private VerticalLayoutGroup contentVerticalLayoutGroup;
    [SerializeField] private PauseManager pauseManager;
    private GameStats currentGameStats;
    private GameObject[] bufferQuests;
    private RectTransform contentRectTransform;
    private int[] questOrder;
    private int questNumber, questIndex;

    private void Awake()
    {
        noQuestsYetText.gameObject.SetActive(false);
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
        questTasks.UpdateTasks();
        questNumber = 0;
        for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
        {
            if (questStagesInfo._QuestStages[i] == QuestStages.inProgress || questStagesInfo._QuestStages[i] == QuestStages.canFinish)
            {
                questNumber++;
            }
        }
        if (questNumber != 0)
        {
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, questNumber * (playerQuestPrefab.GetComponent<RectTransform>().sizeDelta.y + contentVerticalLayoutGroup.spacing) - contentVerticalLayoutGroup.spacing);
            questIndex = 0;
            questOrder = new int[questNumber]; 
            for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
            {
                if (questStagesInfo._QuestStages[i] == QuestStages.canFinish)
                {
                    questOrder[questIndex] = i;
                    questIndex++;
                }
            }
            for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
            {
                if (questStagesInfo._QuestStages[i] == QuestStages.inProgress)
                {
                    questOrder[questIndex] = i;
                    questIndex++;
                }
            }
            questIndex = 0;
            bufferQuests = new GameObject[questNumber];       
            for (int i = 0; i < questStagesInfo._QuestStages.Length; i++)
            {
                if (questStagesInfo._QuestStages[i] == QuestStages.inProgress || questStagesInfo._QuestStages[i] == QuestStages.canFinish)
                {
                    bufferQuests[questIndex] = Instantiate(playerQuestPrefab, contentRectTransform);
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[0].text = questsInfo._QuestInfo[questOrder[questIndex]].name;
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[1].text = $"Task: {questsInfo._QuestInfo[questOrder[questIndex]].taskDescription}.";
                    float bufferProgressPercent = 0;
                    if (questOrder[questIndex] == questTaskVariableValuesInfo.BanditKillerIndex)
                    {
                        bufferProgressPercent = (float)currentGameStats.questVariableStats.banditKillerKilledBanditNumber / (float)questTaskVariableValuesInfo.BanditKillerBanditNumber;
                    }
                    else if (questOrder[questIndex] == questTaskVariableValuesInfo.HeroOfTheForestIndex)
                    {
                        if (currentGameStats.questVariableStats.killedForestDragon)
                        {
                            bufferProgressPercent = 1f;
                        }
                        else
                        {
                            bufferProgressPercent = 0f;
                        }
                    }
                    else if (questOrder[questIndex] == questTaskVariableValuesInfo.SporeWarIndex)
                    {
                        bufferProgressPercent = (float)currentGameStats.questVariableStats.sporeWarKilledMushroomNumber / (float)questTaskVariableValuesInfo.SporeWarMushroomNumber;
                    }
                    else if (questOrder[questIndex] == questTaskVariableValuesInfo.GoblinTroubleIndex)
                    {
                        bufferProgressPercent = (float)currentGameStats.questVariableStats.goblinTroubleKilledGoblinNumber / (float)questTaskVariableValuesInfo.GoblinTroubleGoblinNumber;
                    }
                    else
                    {
                        bufferProgressPercent = 1;
                    }
                    bufferProgressPercent *= 100;
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[2].text = $"Progress: {Mathf.FloorToInt(bufferProgressPercent)}%";
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[3].text = $"Reward: {questsInfo._QuestInfo[questOrder[questIndex]].rewardDescription}.";
                    if (questStagesInfo._QuestStages[questOrder[questIndex]] == QuestStages.inProgress)
                    {
                        bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[4].text = "In progress";
                    }
                    else if (questStagesInfo._QuestStages[questOrder[questIndex]] == QuestStages.canFinish)
                    {
                        bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[4].text = "Can finish";
                    }
                    questIndex++;
                }
            }
        }
        else
        {
            noQuestsYetText.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        noQuestsYetText.gameObject.SetActive(false);
        if (bufferQuests != null)
        {
            for (int i = 0; i < bufferQuests.Length; i++)
            {
                Destroy(bufferQuests[i]);
            }
        }
    }

    public void CloseMenuButton()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseManager.SetGameNotPaused();
        // StaticEffects.coroutines.gameObject.SetActive(true);
    }
}