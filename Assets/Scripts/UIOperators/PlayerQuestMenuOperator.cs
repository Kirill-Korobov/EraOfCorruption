using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestMenuOperator : MonoBehaviour
{
    [SerializeField] private QuestStagesInfo questStagesInfo;
    [SerializeField] private QuestsInfo questsInfo;
    [SerializeField] private GameObject playerQuestPrefab, content;
    [SerializeField] private TMP_Text noQuestsYetText;
    [SerializeField] private VerticalLayoutGroup contentVerticalLayoutGroup;
    private GameObject[] bufferQuests;
    private RectTransform contentRectTransform;
    private int[] questOrder;
    private int questNumber, questIndex;

    private void Awake()
    {
        noQuestsYetText.gameObject.SetActive(false);
        contentRectTransform = content.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        questNumber = 0;
        for (int i = 0; i < questStagesInfo.questStages.Length; i++)
        {
            if (questStagesInfo.questStages[i] == QuestStages.inProgress || questStagesInfo.questStages[i] == QuestStages.canFinish)
            {
                questNumber++;
            }
        }
        if (questNumber != 0)
        {
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, questNumber * (playerQuestPrefab.GetComponent<RectTransform>().sizeDelta.y + contentVerticalLayoutGroup.spacing) - contentVerticalLayoutGroup.spacing);
            questIndex = 0;
            questOrder = new int[questNumber];
            for (int i = 0; i < questStagesInfo.questStages.Length; i++)
            {
                if (questStagesInfo.questStages[i] == QuestStages.inProgress)
                {
                    questOrder[questIndex] = i;
                    questIndex++;
                }
            }
            for (int i = 0; i < questStagesInfo.questStages.Length; i++)
            {
                if (questStagesInfo.questStages[i] == QuestStages.canFinish)
                {
                    questOrder[questIndex] = i;
                    questIndex++;
                }
            }
            questIndex = 0;
            bufferQuests = new GameObject[questNumber];       
            for (int i = 0; i < questStagesInfo.questStages.Length; i++)
            {
                if (questStagesInfo.questStages[i] == QuestStages.inProgress || questStagesInfo.questStages[i] == QuestStages.canFinish)
                {
                    bufferQuests[questIndex] = Instantiate(playerQuestPrefab, contentRectTransform);
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[0].text = questsInfo._QuestInfo[questOrder[questIndex]].name;
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[1].text = $"Task: {questsInfo._QuestInfo[questOrder[questIndex]].taskDescription}.";
                    bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[2].text = $"Reward: {questsInfo._QuestInfo[questOrder[questIndex]].rewardDescription}.";
                    if (questStagesInfo.questStages[questOrder[questIndex]] == QuestStages.inProgress)
                    {
                        bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[3].text = "In progress";
                    }
                    else if (questStagesInfo.questStages[questOrder[questIndex]] == QuestStages.canFinish)
                    {
                        bufferQuests[questIndex].GetComponentsInChildren<TMP_Text>()[3].text = "Can finish";
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
        StaticEffects.coroutines.gameObject.SetActive(true);
    }
}