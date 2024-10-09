using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement : MonoBehaviour
{
    public string name { get; private set; }
    public string description { get; private set; }

    public bool unlocked = false;

}
