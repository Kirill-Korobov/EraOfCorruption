using UnityEngine;

public class NicknameCanvasBehaviour : MonoBehaviour
{
    private Transform mainCharacterTransform;
    

    private void Awake()
    {
        mainCharacterTransform = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(mainCharacterTransform.position); 
    }
}