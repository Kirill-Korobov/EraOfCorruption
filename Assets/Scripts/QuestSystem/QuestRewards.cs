using UnityEngine;

public class QuestRewards : MonoBehaviour
{
    private int questIndex;
    [SerializeField] private QuestRewardVariableValuesInfo rewardVariableValuesInfo;
    [SerializeField] private SpawnForestDragon spawnForestDragon;
    [SerializeField] private NPCMenuOperator _NPCMenuOperator;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private GameObject darkServant;
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
    }

    public void GetReward(int _questIndex)
    {
        questIndex = _questIndex;
        switch (questIndex)
        {
            case 0:
                // give apples
                break;
            case 1:
                // give a wooden bow and wooden arrows
                break;
            case 2:
                // give a stone sword and money
                break;
            case 3:
                // give a wooden staff
                break;
            case 4:
                statisticsManager.ResetStats();
                _NPCMenuOperator.FinishInteraction();
                break;
            case 5:
                // give money
                currentGameStats.questVariableStats.banditKillerKilledBanditNumber = 0;
                Debug.Log("Bandit killer quest reward received.");
                break;
            case 6:
                // give a dessert location pass and money
                Debug.Log("Hero of the forest quest reward received.");
                break;
            case 7:
                // give money
                currentGameStats.questVariableStats.sporeWarKilledMushroomNumber = 0;
                Debug.Log("Spore war quest reward received.");
                break;
            case 8:
                // give money
                currentGameStats.questVariableStats.goblinTroubleKilledGoblinNumber = 0;
                Debug.Log("Goblin trouble quest reward received.");
                break;
            case 9:
                spawnForestDragon.Spawn();
                _NPCMenuOperator.FinishInteraction();
                darkServant.SetActive(false);
                break;
            default:
                Debug.Log("No reward for this quest.");
                break;
        }
    }
}