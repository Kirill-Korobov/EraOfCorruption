using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueMenuOperator : MonoBehaviour
{
    [SerializeField] private NPCMenuOperator _NPCMenuOperator;
    [SerializeField] private GameObject mainCharacterSpeakerNameGameObject, _NPCSpeakerGameObject;
    [SerializeField] private TMP_Text dialogueText, mainCharacterSpeakerNameText, _NPCSpeakerNameText;
    [SerializeField] private NPCsInfo _NPCsInfo;
    [SerializeField] private DialoguesInfo dialoguesInfo;
    [SerializeField] private float spawnReplicaCharInterval;
    private Coroutine updateDialogueTextCoroutine;
    private int interactingNPCID, currentReplicaIndex;
    private bool dialogueIsPlaying;

    private void Awake()
    {
        dialogueIsPlaying = false;
        interactingNPCID = -1;
    }

    private void Update()
    {
        if (dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FinishDialogue();
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                currentReplicaIndex++;
                if (currentReplicaIndex > dialoguesInfo._DialoguesInfo[interactingNPCID].dialogue.Length - 1)
                {
                    FinishDialogue();
                }
                else
                {
                    if (updateDialogueTextCoroutine != null)
                    {
                        StopCoroutine(updateDialogueTextCoroutine);
                    }
                    updateDialogueTextCoroutine = StartCoroutine(UpdateDialogueTextCoroutine(dialoguesInfo._DialoguesInfo[interactingNPCID].dialogue[currentReplicaIndex].replicaText));
                    if (dialoguesInfo._DialoguesInfo[interactingNPCID].dialogue[currentReplicaIndex].speaker == Speaker.MainCharacter)
                    {
                        mainCharacterSpeakerNameGameObject.SetActive(true);
                        _NPCSpeakerGameObject.SetActive(false);
                    }
                    else
                    {
                        mainCharacterSpeakerNameGameObject.SetActive(false);
                        _NPCSpeakerGameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void PlayDialogue(int _interactingNPCID)
    {
        dialogueIsPlaying = true;
        interactingNPCID = _interactingNPCID;
        mainCharacterSpeakerNameText.text = "You";
        _NPCSpeakerNameText.text = _NPCsInfo._NPCsInfo[interactingNPCID].name;
        currentReplicaIndex = 0;
        updateDialogueTextCoroutine = StartCoroutine(UpdateDialogueTextCoroutine(dialoguesInfo._DialoguesInfo[interactingNPCID].dialogue[currentReplicaIndex].replicaText));
        if (dialoguesInfo._DialoguesInfo[interactingNPCID].dialogue[currentReplicaIndex].speaker == Speaker.MainCharacter)
        {
            mainCharacterSpeakerNameGameObject.SetActive(true);
            _NPCSpeakerGameObject.SetActive(false);
        }
        else
        {
            mainCharacterSpeakerNameGameObject.SetActive(false);
            _NPCSpeakerGameObject.SetActive(true);
        }
    }

    private IEnumerator UpdateDialogueTextCoroutine(string replicaText)
    {
        string bufferText = string.Empty;
        for (int i = 0; i < replicaText.Length; i++)
        {
            bufferText += replicaText[i];
            dialogueText.text = bufferText;
            yield return new WaitForSecondsRealtime(spawnReplicaCharInterval);
        }
    }

    public void FinishDialogue()
    {
        dialogueIsPlaying = false;
        interactingNPCID = -1;
        _NPCMenuOperator.CloseDialogueMenu();
    }
}