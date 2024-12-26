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
    [SerializeField] private Vector2 slot1Position, slot2Position, slot3Position;
    private GameObject slot1, slot2, slot3;
    private int deleteGameConfirmationPassword;

    private void OnEnable()
    {
        if (gameStatsManager.game1Stats.gameIsCreated)
        {
            slot1 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot1.GetComponent<RectTransform>().anchoredPosition = slot1Position;
            slot1.GetComponent<Button>().onClick.AddListener(() => LoadGame());
            slot1.GetComponentsInChildren<TMP_Text>()[0].text = gameStatsManager.game1Stats.gameName;
            slot1.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: " + gameStatsManager.game1Stats.gameDifficulty;
            slot1.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(1));
        }
        else
        {
            slot1 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot1.GetComponent<RectTransform>().anchoredPosition = slot1Position;
            slot1.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(1));
        }

        if (gameStatsManager.game2Stats.gameIsCreated)
        {
            slot2 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot2.GetComponent<RectTransform>().anchoredPosition = slot2Position;
            slot2.GetComponent<Button>().onClick.AddListener(() => LoadGame());
            slot2.GetComponentsInChildren<TMP_Text>()[0].text = gameStatsManager.game2Stats.gameName;
            slot2.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: " + gameStatsManager.game2Stats.gameDifficulty;
            slot2.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(2));
        }
        else
        {
            slot2 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot2.GetComponent<RectTransform>().anchoredPosition = slot2Position;
            slot2.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(2));
        }

        if (gameStatsManager.game3Stats.gameIsCreated)
        {
            slot3 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot3.GetComponent<RectTransform>().anchoredPosition = slot3Position;
            slot3.GetComponent<Button>().onClick.AddListener(() => LoadGame());
            slot3.GetComponentsInChildren<TMP_Text>()[0].text = gameStatsManager.game3Stats.gameName;
            slot3.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: " + gameStatsManager.game3Stats.gameDifficulty;
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

    public void LoadGame()
    {
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