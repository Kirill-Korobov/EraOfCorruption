using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseGameOperator : MonoBehaviour
{
    public int selectedGameNumber;
    [SerializeField] private GameObject startMenuContent, emptySlotPrefab, filledSlotPrefab, createGameWindow, deleteGameConfirmationWindow;
    [SerializeField] private Toggle deleteGameConfirmationToggle;
    [SerializeField] private TMP_Text deleteGameConfirmationPasswordText;
    [SerializeField] private InputField deleteGameConfirmationInputField;
    [SerializeField] private Vector2 slot1Position, slot2Position, slot3Position;
    private GameObject slot1, slot2, slot3;
    private int deleteGameConfirmationPassword;

    private void OnEnable()
    {     
        if (GameStats.game1_GameIsCreated)
        {
            slot1 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot1.GetComponent<RectTransform>().anchoredPosition = slot1Position;
            slot1.GetComponent<Button>().onClick.AddListener(() => LoadGame());
            slot1.GetComponentsInChildren<TMP_Text>()[0].text = GameStats.game1_GameName;
            slot1.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: " + GameStats.game1_Difficulty;
            slot1.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(1));
        }
        else
        {
            slot1 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot1.GetComponent<RectTransform>().anchoredPosition = slot1Position;
            slot1.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(1));
        }


        if (GameStats.game2_GameIsCreated)
        {
            slot2 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot2.GetComponent<RectTransform>().anchoredPosition = slot2Position;
            slot2.GetComponent<Button>().onClick.AddListener(() => LoadGame());
            slot2.GetComponentsInChildren<TMP_Text>()[0].text = GameStats.game2_GameName;
            slot2.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: " + GameStats.game2_Difficulty;
            slot2.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => RubbishBinButton(2));
        }
        else
        {
            slot2 = Instantiate(emptySlotPrefab, gameObject.transform);
            slot2.GetComponent<RectTransform>().anchoredPosition = slot2Position;
            slot2.GetComponent<Button>().onClick.AddListener(() => OpenCreateGameWindow(2));
        }

        if (GameStats.game3_GameIsCreated)
        {
            slot3 = Instantiate(filledSlotPrefab, gameObject.transform);
            slot3.GetComponent<RectTransform>().anchoredPosition = slot3Position;
            slot3.GetComponent<Button>().onClick.AddListener(() => LoadGame());
            slot3.GetComponentsInChildren<TMP_Text>()[0].text = GameStats.game3_GameName;
            slot3.GetComponentsInChildren<TMP_Text>()[1].text = "Difficulty: " + GameStats.game3_Difficulty;
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
            GameStats.game1_GameIsCreated = false;
            GameStats.game1_GameName = string.Empty;
            GameStats.game1_Difficulty = GameStats.GameDifficulty.medium;
        }
        else if (selectedGameNumber == 2)
        {
            GameStats.game2_GameIsCreated = false;
            GameStats.game2_GameName = string.Empty;
            GameStats.game2_Difficulty = GameStats.GameDifficulty.medium;
        }
        else if (selectedGameNumber == 3)
        {
            GameStats.game3_GameIsCreated = false;
            GameStats.game3_GameName = string.Empty;
            GameStats.game3_Difficulty = GameStats.GameDifficulty.medium;
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
        SceneManager.LoadScene("Loading");
    }

    public void OpenCreateGameWindow(int selectedGameNumber)
    {
        this.selectedGameNumber = selectedGameNumber;
        gameObject.SetActive(false);
        createGameWindow.SetActive(true);
    }
}