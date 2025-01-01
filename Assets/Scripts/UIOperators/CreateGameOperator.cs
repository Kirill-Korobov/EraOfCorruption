using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameOperator : MonoBehaviour
{
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private ChooseGameOperator chooseGameOperator;
    [SerializeField] private GameObject chooseGameWindow;
    [SerializeField] private InputField enterGameNameInputField;
    [SerializeField] private TMP_Text easyDifficultyText, mediumDifficultyText, hardDifficultyText, crazyDifficultyText, incorrectInputErrorText;
    [SerializeField] private DifficultyColorsInfo difficultyColorsInfo;
    [SerializeField] private int gameNameMaxLength;
    private Coroutine showIncorrectInputTextCoroutine;
    private string currentGameName;
    private GameDifficulty currentGameDifficulty;

    private void OnEnable()
    {
        enterGameNameInputField.text = string.Empty;
        currentGameDifficulty = GameDifficulty.medium;
        easyDifficultyText.gameObject.SetActive(false);
        mediumDifficultyText.gameObject.SetActive(true);
        hardDifficultyText.gameObject.SetActive(false);
        crazyDifficultyText.gameObject.SetActive(false);
        easyDifficultyText.color = difficultyColorsInfo.EasyDifficultyColor;
        easyDifficultyText.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.EasyDifficultyColor;
        mediumDifficultyText.color = difficultyColorsInfo.MediumDifficultyColor;
        mediumDifficultyText.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.MediumDifficultyColor;
        hardDifficultyText.color = difficultyColorsInfo.HardDifficultyColor;
        hardDifficultyText.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.HardDifficultyColor;
        crazyDifficultyText.color = difficultyColorsInfo.CrazyDifficultyColor; 
        crazyDifficultyText.GetComponentsInChildren<TMP_Text>()[1].color = difficultyColorsInfo.CrazyDifficultyColor;
    }

    public void PreviousDifficultyButton()
    {
        if (currentGameDifficulty == GameDifficulty.eazy)
        {
            currentGameDifficulty = GameDifficulty.crazy;
            easyDifficultyText.gameObject.SetActive(false);
            crazyDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameDifficulty.medium)
        {
            currentGameDifficulty = GameDifficulty.eazy;
            mediumDifficultyText.gameObject.SetActive(false);
            easyDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameDifficulty.hard)
        {
            currentGameDifficulty = GameDifficulty.medium;
            hardDifficultyText.gameObject.SetActive(false);
            mediumDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameDifficulty.crazy)
        {
            currentGameDifficulty = GameDifficulty.hard;
            crazyDifficultyText.gameObject.SetActive(false);
            hardDifficultyText.gameObject.SetActive(true);
        }
    }

    public void NextDifficultyButton()
    {
        if (currentGameDifficulty == GameDifficulty.eazy)
        {
            currentGameDifficulty = GameDifficulty.medium;
            easyDifficultyText.gameObject.SetActive(false);
            mediumDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameDifficulty.medium)
        {
            currentGameDifficulty = GameDifficulty.hard;
            mediumDifficultyText.gameObject.SetActive(false);
            hardDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameDifficulty.hard)
        {
            currentGameDifficulty = GameDifficulty.crazy;
            hardDifficultyText.gameObject.SetActive(false);
            crazyDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameDifficulty.crazy)
        {
            currentGameDifficulty = GameDifficulty.eazy;
            crazyDifficultyText.gameObject.SetActive(false);
            easyDifficultyText.gameObject.SetActive(true);
        }
    }

    public void ChangeCurrentGameName()
    {
        currentGameName = enterGameNameInputField.text;
    }

    public void CreateGameButton()
    {
        if (currentGameName != null && currentGameName.Length != 0 && currentGameName.Length <= gameNameMaxLength)
        {
            if (chooseGameOperator.selectedGameNumber == 1)
            {
                gameStatsManager.game1Stats.slotStats.gameIsCreated = true;
                gameStatsManager.game1Stats.slotStats.gameName = currentGameName;
                gameStatsManager.game1Stats.slotStats.gameDifficulty = currentGameDifficulty;
            }
            else if (chooseGameOperator.selectedGameNumber == 2)
            {
                gameStatsManager.game2Stats.slotStats.gameIsCreated = true;
                gameStatsManager.game2Stats.slotStats.gameName = currentGameName;
                gameStatsManager.game2Stats.slotStats.gameDifficulty = currentGameDifficulty;
            }
            else if (chooseGameOperator.selectedGameNumber == 3)
            {
                gameStatsManager.game3Stats.slotStats.gameIsCreated = true;
                gameStatsManager.game3Stats.slotStats.gameName = currentGameName;
                gameStatsManager.game3Stats.slotStats.gameDifficulty = currentGameDifficulty;
            }
            CloseCreateGameWindow();
        }
        else
        {
            if (showIncorrectInputTextCoroutine != null)
            {
                StopCoroutine(showIncorrectInputTextCoroutine);
            }
            showIncorrectInputTextCoroutine = StartCoroutine(incorrectInputErrorText.GetComponent<IncorrectInputErrorTextBehaviour>().ShowIncorrectInputText());
        }
    }

    public void CloseCreateGameWindow()
    {
        gameObject.SetActive(false);
        chooseGameWindow.SetActive(true);
    }
}