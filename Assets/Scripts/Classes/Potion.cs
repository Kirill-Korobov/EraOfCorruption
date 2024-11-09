using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum potionEffect { }
    public string name {  get; set; }
    public potionEffect effect { get; set;}
    public float duration { get; set; }

    public Potion(string name, potionEffect effect, float duration)
    {
        this.name = name;
        this.effect = effect;
        this.duration = duration;
    }
}
