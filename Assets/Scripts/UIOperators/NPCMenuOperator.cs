using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCMenuOperator : MonoBehaviour
{
    [SerializeField] private DialogueMenuOperator dialogueMenuOperator;
    [SerializeField] private NPCQuestMenuOperator _NPCQuestMenuOperator;
    [SerializeField] private NPCInteraction _NPCInteraction;
    [SerializeField] private GameObject startMenu, dialogueMenu, questMenu, tradeMenu;
    [SerializeField] private NPCsInfo _NPCsInfo;
    [SerializeField] private GameObject startMenuButtonsPrefab;
    [SerializeField] private Vector2 twoButtonsStartSpawnPosition, threeButtonsStartSpawnPosition;
    [SerializeField] private float distanceBetweenButtons;
    private GameObject[] startMenuSpawnedButtons;
    private int interactingNPCID;

    private void Awake()
    {
        interactingNPCID = -1;
        gameObject.SetActive(false);
        startMenu.SetActive(false);
        DeleteStartMenuSpawnedButtons();
        dialogueMenu.SetActive(false);
        questMenu.SetActive(false);
        tradeMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeSelf && (startMenu.activeSelf || (gameObject.activeSelf && _NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length == 0 && _NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes.Length == 0)))
            {
                FinishInteraction();
            }
            else if (dialogueMenu.activeSelf || questMenu.activeSelf || tradeMenu.activeSelf)
            {
                OpenStartMenu();
                dialogueMenu.SetActive(false);
                questMenu.SetActive(false);
                tradeMenu.SetActive(false);
            }
        }
    }

    public void StartInteraction(int _NPCID)
    {
        gameObject.SetActive(true);
        interactingNPCID = _NPCID;
        if (_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length == 0 && _NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes.Length == 0)
        {
            OpenDialogueMenu();
        }
        else
        {
            OpenStartMenu();
        }
    }

    public void OpenStartMenu()
    {
        dialogueMenu.SetActive(false);
        questMenu.SetActive(false);
        tradeMenu.SetActive(false);
        startMenu.SetActive(true);
        if (_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length != 0 && _NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes.Length != 0)
        {
            startMenuSpawnedButtons = new GameObject[3];

            startMenuSpawnedButtons[0] = Instantiate(startMenuButtonsPrefab, startMenu.transform);
            startMenuSpawnedButtons[0].GetComponent<RectTransform>().anchoredPosition = threeButtonsStartSpawnPosition;
            startMenuSpawnedButtons[0].GetComponent<Button>().onClick.AddListener(() => OpenDialogueMenu());
            startMenuSpawnedButtons[0].GetComponentInChildren<TMP_Text>().text = "Dialogue";

            startMenuSpawnedButtons[1] = Instantiate(startMenuButtonsPrefab, startMenu.transform);
            startMenuSpawnedButtons[1].GetComponent<RectTransform>().anchoredPosition = threeButtonsStartSpawnPosition + new Vector2(distanceBetweenButtons, 0);
            startMenuSpawnedButtons[1].GetComponent<Button>().onClick.AddListener(() => OpenQuestMenu());
            startMenuSpawnedButtons[1].GetComponentInChildren<TMP_Text>().text = "Quests";

            startMenuSpawnedButtons[2] = Instantiate(startMenuButtonsPrefab, startMenu.transform);
            startMenuSpawnedButtons[2].GetComponent<RectTransform>().anchoredPosition = threeButtonsStartSpawnPosition + 2 * new Vector2(distanceBetweenButtons, 0);
            startMenuSpawnedButtons[2].GetComponent<Button>().onClick.AddListener(() => OpenTradeMenu());
            startMenuSpawnedButtons[2].GetComponentInChildren<TMP_Text>().text = "Trades";
        }
        else
        {
            startMenuSpawnedButtons = new GameObject[2];

            startMenuSpawnedButtons[0] = Instantiate(startMenuButtonsPrefab, startMenu.transform);
            startMenuSpawnedButtons[0].GetComponent<RectTransform>().anchoredPosition = twoButtonsStartSpawnPosition;
            startMenuSpawnedButtons[0].GetComponent<Button>().onClick.AddListener(() => OpenDialogueMenu());
            startMenuSpawnedButtons[0].GetComponentInChildren<TMP_Text>().text = "Dialogue";

            if (_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length != 0)
            {
                startMenuSpawnedButtons[1] = Instantiate(startMenuButtonsPrefab, startMenu.transform);
                startMenuSpawnedButtons[1].GetComponent<RectTransform>().anchoredPosition = twoButtonsStartSpawnPosition + new Vector2(distanceBetweenButtons, 0);
                startMenuSpawnedButtons[1].GetComponent<Button>().onClick.AddListener(() => OpenQuestMenu());
                startMenuSpawnedButtons[1].GetComponentInChildren<TMP_Text>().text = "Quests";
            }
            else
            {
                startMenuSpawnedButtons[1] = Instantiate(startMenuButtonsPrefab, startMenu.transform);
                startMenuSpawnedButtons[1].GetComponent<RectTransform>().anchoredPosition = twoButtonsStartSpawnPosition + new Vector2(distanceBetweenButtons, 0);
                startMenuSpawnedButtons[1].GetComponent<Button>().onClick.AddListener(() => OpenTradeMenu());
                startMenuSpawnedButtons[1].GetComponentInChildren<TMP_Text>().text = "Trades";
            }
        }
    }

    private void OpenDialogueMenu()
    {
        startMenu.SetActive(false);
        DeleteStartMenuSpawnedButtons();
        dialogueMenu.SetActive(true);
        dialogueMenuOperator.PlayDialogue(interactingNPCID);
    }

    public void CloseDialogueMenu()
    {
        if (_NPCsInfo._NPCsInfo[interactingNPCID].questIndexes.Length == 0 && _NPCsInfo._NPCsInfo[interactingNPCID].tradesIndexes.Length == 0)
        {
            FinishInteraction();
        }
        else
        {
            OpenStartMenu();
        }
    }

    private void OpenQuestMenu()
    {
        startMenu.SetActive(false);
        DeleteStartMenuSpawnedButtons();
        questMenu.SetActive(true);
        _NPCQuestMenuOperator.SpawnQuests(interactingNPCID);
        _NPCQuestMenuOperator.DeactivateRequirementsNotMetText();
    }

    public void CloseQuestMenu()
    {
        _NPCQuestMenuOperator.DeleteQuests();
        _NPCQuestMenuOperator.DeactivateRequirementsNotMetText();
        OpenStartMenu();
    }

    private void OpenTradeMenu()
    {
        startMenu.SetActive(false);
        DeleteStartMenuSpawnedButtons();
        tradeMenu.SetActive(true);
    }

    private void DeleteStartMenuSpawnedButtons()
    {
        if (startMenuSpawnedButtons != null)
        {
            for (int i = 0; i < startMenuSpawnedButtons.Length; i++)
            {
                Destroy(startMenuSpawnedButtons[i]);
            }
        }
    }

    public void FinishInteraction()
    {
        interactingNPCID = -1;
        gameObject.SetActive(false);
        startMenu.SetActive(false);
        DeleteStartMenuSpawnedButtons();
        _NPCInteraction.FinishInteraction();
    }
}