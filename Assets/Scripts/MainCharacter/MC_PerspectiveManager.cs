using UnityEngine;

public class MC_PerspectiveManager : MonoBehaviour
{
    [SerializeField] private Camera firstPersonPerspectiveCamera, secondPersonPerspectiveCamera, thirdPersonPerspectiveCamera;
    private int currentPerspective;

    private void Awake()
    {
        firstPersonPerspectiveCamera.gameObject.SetActive(false);
        secondPersonPerspectiveCamera.gameObject.SetActive(false);
        thirdPersonPerspectiveCamera.gameObject.SetActive(false);
        // Set saved camera perspective. 
        firstPersonPerspectiveCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (currentPerspective != 3)
            {
                currentPerspective++;
            }
            else
            {
                currentPerspective = 1;
            }
            SetPerspective();
        }
    }

    private void SetPerspective()
    {
        switch (currentPerspective)
        {
            case 1:
                thirdPersonPerspectiveCamera.gameObject.SetActive(false);
                firstPersonPerspectiveCamera.gameObject.SetActive(true);
                break;
            case 2:
                firstPersonPerspectiveCamera.gameObject.SetActive(false);
                secondPersonPerspectiveCamera.gameObject.SetActive(true);
                break;
            case 3:
                secondPersonPerspectiveCamera.gameObject.SetActive(false);
                thirdPersonPerspectiveCamera.gameObject.SetActive(true);
                break;
        }
    }
}