using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TakeItemManager : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    void Update()
    {
        if (Input.GetKeyDown(LoadedSettings.take))
        {
            RaycastHit hit; 
            if (Physics.Raycast(VirtualCamera.transform.position, VirtualCamera.transform.forward, out hit, 5)) 
            {
                TakeItem ti;
                if (hit.collider.gameObject.TryGetComponent(out ti))
                {
                    ti.Take();
                }
            } 
        }
    }
}
