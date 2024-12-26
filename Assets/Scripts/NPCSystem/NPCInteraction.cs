using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private float maxInteractionDistance;
    [SerializeField] private NPCMenuOperator _NPCMenuOperator;
    [SerializeField] private PauseManager pauseManager;
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
        if (Input.GetKeyDown(KeyCode.Return) && !isInteracting)
        {
            for (int i = 0; i < NPCs.Length; i++)
            {
                if (i == 0 || Vector3.Distance(transform.position, NPCs[i].transform.position) < Vector3.Distance(transform.position, nearestNPC.transform.position))
                {
                    nearestNPC = NPCs[i];
                }
            }
            if (Vector3.Distance(transform.position, nearestNPC.transform.position) <= maxInteractionDistance)
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
        isInteracting = false;
        interactingNPC = null;
        pauseManager.SetGameNotPaused();
    }
}