using UnityEngine;

public class MainGameUIOperator : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas, pauseCanvas, inventoryCanvas, statisticsCanvas, mapCanvas, questCanvas, _NPCQuestCanvas;

    private void Awake()
    {
        mainCanvas.gameObject.SetActive(true);
        SetAllCanvasesInactive();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.gameObject.activeSelf || inventoryCanvas.gameObject.activeSelf || statisticsCanvas.gameObject.activeSelf || mapCanvas.gameObject.activeSelf || questCanvas.gameObject.activeSelf)
            {
                SetAllCanvasesInactive();
            }
            else if (!_NPCQuestCanvas.gameObject.activeSelf)
            {
                pauseCanvas.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!inventoryCanvas.gameObject.activeSelf)
            {
                SetAllCanvasesInactive();
                inventoryCanvas.gameObject.SetActive(true);
            }
            else
            {
                inventoryCanvas.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!mapCanvas.gameObject.activeSelf)
            {
                SetAllCanvasesInactive();
                mapCanvas.gameObject.SetActive(true);
            }
            else
            {
                mapCanvas.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!statisticsCanvas.gameObject.activeSelf)
            {
                SetAllCanvasesInactive();
                statisticsCanvas.gameObject.SetActive(true);
            }
            else
            {
                statisticsCanvas.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!questCanvas.gameObject.activeSelf)
            {
                SetAllCanvasesInactive();
                questCanvas.gameObject.SetActive(true);
            }
            else
            {
                questCanvas.gameObject.SetActive(false);
            }
        }
    }

    private void SetAllCanvasesInactive()
    {
        pauseCanvas.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(false);
        statisticsCanvas.gameObject.SetActive(false);
        mapCanvas.gameObject.SetActive(false);
        questCanvas.gameObject.SetActive(false);
    }
}