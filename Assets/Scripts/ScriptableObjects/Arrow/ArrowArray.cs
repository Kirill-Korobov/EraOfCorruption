using UnityEngine;

[CreateAssetMenu(fileName = "ArrowArray", menuName = "ScriptableObjects/ArrowArray")]
public class ArrowArray : ScriptableObject
{
    [SerializeField] private GameObject[] arrows;
    public GameObject[] Arrows { get => arrows; set => arrows = value; }
}
