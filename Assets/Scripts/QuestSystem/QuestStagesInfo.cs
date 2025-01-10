using UnityEngine;

public class QuestStagesInfo : MonoBehaviour
{
    [SerializeField] private GameStatsManager gameStatsManager;
    private GameStats currentGameStats;

    private void Start()
    {
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
        _QuestStages = currentGameStats.questStagesStats.questStages;
    }

    public QuestStages[] _QuestStages
    {
        get
        {
            return currentGameStats.questStagesStats.questStages;
        }
        set
        {
            currentGameStats.questStagesStats.questStages = value;
        }
    }
}