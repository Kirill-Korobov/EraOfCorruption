using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundSpritesInfoConfig", menuName = "ScriptableObjects/BackgroundSpritesInfo")]

public class BackgroundSpritesInfo : ScriptableObject
{
    [SerializeField] private Sprite[] backgroundSprites;
    public Sprite[] BackgroundSprites => backgroundSprites;
}