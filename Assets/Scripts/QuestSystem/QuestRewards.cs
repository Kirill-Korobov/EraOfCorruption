using UnityEngine;

public class QuestRewards : MonoBehaviour
{
    private int questIndex;
    [SerializeField] private SpawnForestDragon spawnForestDragon;
    [SerializeField] private NPCMenuOperator _NPCMenuOperator;

    public void GetReward(int _questIndex)
    {
        questIndex = _questIndex;
        // Прописати логіку винагороди для кожного квеста.
        switch (questIndex)
        {
            case 0:
                spawnForestDragon.Spawn();
                _NPCMenuOperator.FinishInteraction();
                break;
            default:
                Debug.Log("No reward for this quest.");
                break;
        }
    }
}