using Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MC_PerspectiveManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera firstPersonVirtualCamera, secondPersonVirtualCamera, thirdPersonVirtualCamera;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private GameStatsManager gameStatsManager;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject aim, model;
    private Cinemachine3rdPersonFollow secondPersonVirtualCamera3rdPersonFollow, thirdPersonVirtualCamera3rdPersonFollow;
    private GameStats currentGameStats;
    private float rotationY;

    private void Start()
    {
        secondPersonVirtualCamera3rdPersonFollow = secondPersonVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        thirdPersonVirtualCamera3rdPersonFollow = thirdPersonVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        switch (GameStatsManager.currentGame)
        {
            case 1:
                currentGameStats = gameStatsManager.game1Stats;
                break;
            case 2:
                currentGameStats = gameStatsManager.game2Stats;
                break;
            case 3:
                currentGameStats = gameStatsManager.game3Stats;
                break;
            default:
                currentGameStats = gameStatsManager.game1Stats;
                break;
        }
        CurrentPerspective = currentGameStats.mainCharacterStats.currentPerspective;
        ShoulderOffsetZ = currentGameStats.mainCharacterStats.shoulderOffsetZ;
        SetPerspective();
    }

    public int CurrentPerspective
    {
        get
        {
            return currentGameStats.mainCharacterStats.currentPerspective;
        }
        set
        {
            if (value <= 1)
            {
                currentGameStats.mainCharacterStats.currentPerspective = 1;
            }
            else if (value >= 3)
            {
                currentGameStats.mainCharacterStats.currentPerspective = 3;
            }
            else
            {
                currentGameStats.mainCharacterStats.currentPerspective = value;
            }
        }
    }

    public float ShoulderOffsetZ
    {
        get
        {
            return currentGameStats.mainCharacterStats.shoulderOffsetZ;
        }
        set
        {
            if (value <= statisticsInfo.MinShoulderOffsetZValue)
            {
                currentGameStats.mainCharacterStats.shoulderOffsetZ = statisticsInfo.MinShoulderOffsetZValue;
            }
            else if (value >= statisticsInfo.MaxShoulderOffsetZValue)
            {
                currentGameStats.mainCharacterStats.shoulderOffsetZ = statisticsInfo.MaxShoulderOffsetZValue;
            }
            else
            {
                currentGameStats.mainCharacterStats.shoulderOffsetZ = value;
            }
        }
    }

    void Update()
    {
        if (!pauseManager.pause)
        {
            // must be multiplied by sensetivity.
            gameObject.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * 10f);
            if (firstPersonVirtualCamera.Priority > 0)
            {
                // must be multiplied by sensetivity.
                rotationY += Input.GetAxis("Mouse Y") * 10f;
                rotationY = Mathf.Clamp(rotationY, -statisticsInfo.MaxHeadRotationY, statisticsInfo.MaxHeadRotationY);
                head.localEulerAngles = new Vector3(-rotationY, 0, 0);
            }
 
            if (secondPersonVirtualCamera.Priority > 0 || thirdPersonVirtualCamera.Priority > 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                {
                    // must be multiplied by sensetivity.
                    ShoulderOffsetZ += -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1500f;
                }

                secondPersonVirtualCamera3rdPersonFollow.ShoulderOffset = new Vector3(0, ShoulderOffsetZ * statisticsInfo.ShoulderOffsetYMultiplier, ShoulderOffsetZ);
                thirdPersonVirtualCamera3rdPersonFollow.ShoulderOffset = new Vector3(0, ShoulderOffsetZ * statisticsInfo.ShoulderOffsetYMultiplier, -ShoulderOffsetZ);
            }            

            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (CurrentPerspective != 3)
                {
                    CurrentPerspective++;
                }
                else
                {
                    CurrentPerspective = 1;
                }
                SetPerspective();
            }
        }
    }

    private void SetPerspective()
    {
        firstPersonVirtualCamera.Priority = 0;
        secondPersonVirtualCamera.Priority = 0;
        thirdPersonVirtualCamera.Priority = 0;
        switch (CurrentPerspective)
        {
            case 1:
                firstPersonVirtualCamera.Priority = 1;
                model.SetActive(false);
                aim.SetActive(true);
                break;
            case 2:
                secondPersonVirtualCamera.Priority = 1;
                model.SetActive(true);
                aim.SetActive(false);      
                break;
            case 3:
                thirdPersonVirtualCamera.Priority = 1;
                model.SetActive(true);
                aim.SetActive(false); 
                break;
        }
    }
}