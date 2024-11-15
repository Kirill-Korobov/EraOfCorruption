using UnityEngine;

public class MC_Movement : MonoBehaviour
{
    private float speed;

    private void Awake()
    {
        // Set speed.
    }

    private float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if (value > 0)
            {
                speed = value;
            }
        }
    }

    // Write movement functions.
}