using UnityEngine;

public class DevelopersCanvasOperator : MonoBehaviour
{
    [SerializeField] private GameObject startMenuContent;

    public void CloseDevelopersCanvas()
    {
        gameObject.SetActive(false);
        startMenuContent.SetActive(true);
    }
}