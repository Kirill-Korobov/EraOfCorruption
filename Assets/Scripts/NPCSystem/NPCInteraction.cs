using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private NPCMenuOperator _NPCMenuOperator;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    private bool isInteracting;
    private GameObject[] NPCs;
    private GameObject nearestNPC, interactingNPC;

    private void Awake()
    {
        isInteracting = false;
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TryToInteract();
        }
    }

    public void TryToInteract()
    {
        if (!isInteracting && !pauseManager.pause)
        {
            for (int i = 0; i < NPCs.Length; i++)
            {
                if (i == 0 || Vector3.Distance(transform.position, NPCs[i].transform.position) < Vector3.Distance(transform.position, nearestNPC.transform.position))
                {
                    nearestNPC = NPCs[i];
                }
            }
            if (Vector3.Distance(transform.position, nearestNPC.transform.position) <= statisticsInfo.NPCMaxInteractionDistance)
            {
                isInteracting = true;
                _NPCMenuOperator.gameObject.SetActive(true);
                _NPCMenuOperator.StartInteraction(nearestNPC.GetComponent<NPC>().NPCID);
                interactingNPC = nearestNPC;
            }
        }
    }

    public void FinishInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInteracting = false;
        interactingNPC = null;
        pauseManager.SetGameNotPaused();
    }
}