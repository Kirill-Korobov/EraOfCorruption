using UnityEngine;

public class QuestRewards : MonoBehaviour
{
    private int questIndex;

    public void GetReward(int _questIndex)
    {
        questIndex = _questIndex;
        // ��������� ����� ���������� ��� ������� ������.
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