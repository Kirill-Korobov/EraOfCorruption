using UnityEngine;

public class QuestRewards : MonoBehaviour
{
    private int questIndex;

    public void GetReward(int _questIndex)
    {
        questIndex = _questIndex;
        // Прописати логіку винагороди для кожного квеста.
        switch (questIndex)
        {
            case 0:
                break;
            default:
                Debug.Log("No reward for this quest.");
                break;
        }
    }
}