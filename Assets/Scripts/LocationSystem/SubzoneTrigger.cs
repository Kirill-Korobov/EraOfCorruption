using UnityEngine;

public class SubzoneTrigger : MonoBehaviour
{
    private SubzoneTriggerParent subzoneTriggerParent;

    private void Awake()
    {
        subzoneTriggerParent = GetComponentInParent<SubzoneTriggerParent>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("MainCharacter"))
        {
            subzoneTriggerParent.NotifyParent();
        }
    }
}