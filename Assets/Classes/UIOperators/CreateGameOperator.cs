using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameOperator : MonoBehaviour
{
    [SerializeField] private ChooseGameOperator chooseGameOperator;
    [SerializeField] private GameObject chooseGameWindow;
    [SerializeField] private InputField enterGameNameInputField;
    [SerializeField] private TMP_Text easyDifficultyText, mediumDifficultyText, hardDifficultyText, crazyDifficultyText;
    private string currentGameName;
    private GameStats.GameDifficulty currentGameDifficulty;

    private void OnEnable()
    {
        enterGameNameInputField.text = string.Empty;
        currentGameDifficulty = GameStats.GameDifficulty.medium;
        easyDifficultyText.gameObject.SetActive(false);
        mediumDifficultyText.gameObject.SetActive(true);
        hardDifficultyText.gameObject.SetActive(false);
        crazyDifficultyText.gameObject.SetActive(false);
    }

    public void PreviousDifficultyButton()
    {
        if (currentGameDifficulty == GameStats.GameDifficulty.eazy)
        {
            currentGameDifficulty = GameStats.GameDifficulty.crazy;
            easyDifficultyText.gameObject.SetActive(false);
            crazyDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameStats.GameDifficulty.medium)
        {
            currentGameDifficulty = GameStats.GameDifficulty.eazy;
            mediumDifficultyText.gameObject.SetActive(false);
            easyDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameStats.GameDifficulty.hard)
        {
            currentGameDifficulty = GameStats.GameDifficulty.medium;
            hardDifficultyText.gameObject.SetActive(false);
            mediumDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameStats.GameDifficulty.crazy)
        {
            currentGameDifficulty = GameStats.GameDifficulty.hard;
            crazyDifficultyText.gameObject.SetActive(false);
            hardDifficultyText.gameObject.SetActive(true);
        }
    }

    public void NextDifficultyButton()
    {
        if (currentGameDifficulty == GameStats.GameDifficulty.eazy)
        {
            currentGameDifficulty = GameStats.GameDifficulty.medium;
            easyDifficultyText.gameObject.SetActive(false);
            mediumDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameStats.GameDifficulty.medium)
        {
            currentGameDifficulty = GameStats.GameDifficulty.hard;
            mediumDifficultyText.gameObject.SetActive(false);
            hardDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameStats.GameDifficulty.hard)
        {
            currentGameDifficulty = GameStats.GameDifficulty.crazy;
            hardDifficultyText.gameObject.SetActive(false);
            crazyDifficultyText.gameObject.SetActive(true);
        }
        else if (currentGameDifficulty == GameStats.GameDifficulty.crazy)
        {
            currentGameDifficulty = GameStats.GameDifficulty.eazy;
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
        if (currentGameName.Length != 0 && currentGameName.Length <= 20)
        {
            if (chooseGameOperator.selectedGameNumber == 1)
            {
                GameStats.game1_GameIsCreated = true;
                GameStats.game1_GameName = currentGameName;
                GameStats.game1_Difficulty = currentGameDifficulty;
            }
            else if (chooseGameOperator.selectedGameNumber == 2)
            {
                GameStats.game2_GameIsCreated = true;
                GameStats.game2_GameName = currentGameName;
                GameStats.game2_Difficulty = currentGameDifficulty;
            }
            else if (chooseGameOperator.selectedGameNumber == 3)
            {
                GameStats.game3_GameIsCreated = true;
                GameStats.game3_GameName = currentGameName;
                GameStats.game3_Difficulty = currentGameDifficulty;
            }
            CloseCreateGameWindow();
        } 
    }

    public void CloseCreateGameWindow()
    {
        gameObject.SetActive(false);
        chooseGameWindow.SetActive(true);
    }
}