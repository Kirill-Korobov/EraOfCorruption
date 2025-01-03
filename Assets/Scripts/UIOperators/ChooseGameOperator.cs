using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseGameOperator : MonoBehaviour
{
    public int selectedGameNumber;
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private GameObject startMenuContent, emptySlotPrefab, filledSlotPrefab, createGameWindow, deleteGameConfirmationWindow;
    [SerializeField] private Toggle deleteGameConfirmationToggle;
    [SerializeField] private TMP_Text deleteGameConfirmationPasswordText;
    [SerializeField] private InputField deleteGameConfirmationInputField;
    [SerializeField] private DifficultyColorsInfo difficultyColorsInfo;
    [SerializeField] private Vector2 slot1Position, slot2Position, slot3Position;
    private GameObject slot1, slot2, slot3;
    private int deleteGameConfirmationPassword;

    private void OnEnable()
    {
        if (gameStatsManager.game1Stats.slotStats.gameIsCreated)
        {
            slot1 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot1.GetComponent<RectTransform>().anchoredPosition = slot1Position;
            slot1.GetComponent<Button>().onClick.AddListener(() => LoadGame(1));
            slot1.GetComponentsInChildren<TMP_Text>()[0].text = gameStatsManager.game1Stats.slotStats.gameName;
            switch(gameStatsManager.game1Stats.slotStats.gameDifficulty)
            {
                case GameDifficulty.eazy:
                    slot1.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Easy";
                    slot1.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.EasyDifficultyColor;
                    break;
                case GameDifficulty.medium:
                    slot1.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Medium";
                    slot1.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.MediumDifficultyColor;
                    break;
                case GameDifficulty.hard:
                    slot1.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Hard";
                    slot1.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.HardDifficultyColor;
                    break;
                case GameDifficulty.crazy:
                    slot1.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Crazy";
                    slot1.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.CrazyDifficultyColor;
                    break;
            }
            slot1.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(1));
        }
        else
        {
            slot1 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot1.GetComponent<RectTransform>().anchoredPosition = slot1Position;
            slot1.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(1));
        }

        if (gameStatsManager.game2Stats.slotStats.gameIsCreated)
        {
            slot2 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot2.GetComponent<RectTransform>().anchoredPosition = slot2Position;
            slot2.GetComponent<Button>().onClick.AddListener(() => LoadGame(2));
            slot2.GetComponentsInChildren<TMP_Text>()[0].text = gameStatsManager.game2Stats.slotStats.gameName;
            switch (gameStatsManager.game2Stats.slotStats.gameDifficulty)
            {
                case GameDifficulty.eazy:
                    slot2.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Easy";
                    slot2.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.EasyDifficultyColor;
                    break;
                case GameDifficulty.medium:
                    slot2.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Medium";
                    slot2.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.MediumDifficultyColor;
                    break;
                case GameDifficulty.hard:
                    slot2.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Hard";
                    slot2.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.HardDifficultyColor;
                    break;
                case GameDifficulty.crazy:
                    slot2.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Crazy";
                    slot2.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.CrazyDifficultyColor;
                    break;
            }
            slot2.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(2));
        }
        else
        {
            slot2 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot2.GetComponent<RectTransform>().anchoredPosition = slot2Position;
            slot2.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(2));
        }

        if (gameStatsManager.game3Stats.slotStats.gameIsCreated)
        {
            slot3 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot3.GetComponent<RectTransform>().anchoredPosition = slot3Position;
            slot3.GetComponent<Button>().onClick.AddListener(() => LoadGame(3));
            slot3.GetComponentsInChildren<TMP_Text>()[0].text = gameStatsManager.game3Stats.slotStats.gameName;
            switch (gameStatsManager.game3Stats.slotStats.gameDifficulty)
            {
                case GameDifficulty.eazy:
                    slot3.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Easy";
                    slot1.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.EasyDifficultyColor;
                    break;
                case GameDifficulty.medium:
                    slot3.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Medium";
                    slot3.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.MediumDifficultyColor;
                    break;
                case GameDifficulty.hard:
                    slot3.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Hard";
                    slot3.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.HardDifficultyColor;
                    break;
                case GameDifficulty.crazy:
                    slot3.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: Crazy";
                    slot3.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.CrazyDifficultyColor;
                    break;
            }
            slot3.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(3));
        }
        else
        {
            slot3 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot3.GetComponent<RectTransform>().anchoredPosition = slot3Position;
            slot3.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(3));
        }
    }

    private void OnDisable()
    {
        Destroy(slot1);
        Destroy(slot2);
        Destroy(slot3);
    }

    public void RubbishBinButton(int selectedGameNumber)
    {
        this.selectedGameNumber = selectedGameNumber;
        deleteGameConfirmationInputField.text = string.Empty;
        deleteGameConfirmationToggle.isOn = false;
        deleteGameConfirmationPassword = Random.Range(10000000, 100000000);
        deleteGameConfirmationPasswordText.text = "Password: " + deleteGameConfirmationPassword.ToString();
        deleteGameConfirmationWindow.SetActive(true);
    }

    public void DeleteGameButton()
    {
        if (deleteGameConfirmationToggle.isOn && deleteGameConfirmationInputField.text == deleteGameConfirmationPassword.ToString())
        {
            DeleteGame();
        }
        else
        {
            CloseDeleteGameWindow();
        }
    }

    public void DeleteGame()
    {
        if (selectedGameNumber == 1)
        {
            gameStatsManager.game1Stats.SetAllStatsToZero();
        }
        else if (selectedGameNumber == 2)
        {
            gameStatsManager.game2Stats.SetAllStatsToZero();
        }
        else if (selectedGameNumber == 3)
        {
            gameStatsManager.game3Stats.SetAllStatsToZero();
        }
        deleteGameConfirmationWindow.SetActive(false);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void CloseDeleteGameWindow()
    {
        deleteGameConfirmationWindow.SetActive(false);
    }

    public void CloseGameChoice()
    {
        gameObject.SetActive(false);
        startMenuContent.SetActive(true);
    }

    public void LoadGame(int gameNumber)
    {
        GameStatsManager.currentGame = gameNumber;
        gameStatsManager.SaveStats();
        SceneManager.LoadScene("Loading");
    }

    public void OpenCreateGameWindow(int selectedGameNumber)
    {
        this.selectedGameNumber = selectedGameNumber;
        gameObject.SetActive(false);
        createGameWindow.SetActive(true);
    }
}