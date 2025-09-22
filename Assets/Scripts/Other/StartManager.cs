using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] BindButton bb;
    [SerializeField] private MoveSprites[] moveSprites;
    private void Awake()
    {
        for (int i = 0; i < moveSprites.Length; i++)
        {
            moveSprites[i].Set();
        }
    }
}
