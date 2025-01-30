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
            Debug.DrawRay(VirtualCamera.transform.position, VirtualCamera.transform.forward, Color.green, 5);
            if (Physics.Raycast(VirtualCamera.transform.position, VirtualCamera.transform.forward, out RaycastHit hit, 5)) 
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
