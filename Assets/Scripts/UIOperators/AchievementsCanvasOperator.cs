using UnityEngine;

public class AchievementsCanvasOperator : MonoBehaviour
{
    [SerializeField] private GameObject previousMenu;

    public void CloseAchievementsCanvas()
    {
        gameObject.SetActive(false);
        previousMenu.SetActive(true);
    }
}