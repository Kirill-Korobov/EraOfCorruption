using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    private bool ifInTrigger = false;
    public DropedTakedItems dti;
    void Update()
    {
        if (ifInTrigger && Input.GetKeyDown(KeyCode.E)) 
        {
            int a = Random.Range(dti.Min, dti.Max);
            StaticDropTake.sl.AddItem(dti,a);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCharacter")
        {
            ifInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "MainCharacter")
        {
            ifInTrigger = false;
        }
    }
}
