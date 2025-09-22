using UnityEngine;

public class SubzoneTriggerParent : MonoBehaviour
{
    [SerializeField] private LocationManager locationManager;
    [SerializeField] private Sublocation sublocation;

    public void NotifyParent()
    {
        locationManager.ChangeSublocation(sublocation);
    }
}