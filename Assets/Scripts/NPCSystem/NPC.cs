using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCsInfo _NPCsInfo;
    private NicknameCanvasBehaviour _NPCNicknameCanvasBehaviour;
    public int NPCID;

    private void Awake()
    {
        _NPCNicknameCanvasBehaviour = GetComponentInChildren<NicknameCanvasBehaviour>();
        _NPCNicknameCanvasBehaviour.GetComponentInChildren<TMP_Text>().text = _NPCsInfo._NPCsInfo[NPCID].name;
    }
}