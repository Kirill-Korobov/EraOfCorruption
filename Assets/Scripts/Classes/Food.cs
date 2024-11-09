using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public string name { get; set; }
    public int healthPoints { get; set; }

    public Food(string _name, int _healthPoints)
    {
        name = _name;
        healthPoints = _healthPoints;
    }
}
