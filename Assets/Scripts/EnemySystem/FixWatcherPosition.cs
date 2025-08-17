using UnityEngine;

public class FixWatcherPosition : MonoBehaviour
{
    private CloseCombatEnemy closeCombatEnemy;
    private PauseManager pauseManager;
    private bool changePositionDuringPause;
    [SerializeField] private float flightHeightOffset;

    private void Awake()
    {
        closeCombatEnemy = GetComponent<CloseCombatEnemy>();
        pauseManager = FindFirstObjectByType<PauseManager>();
        changePositionDuringPause = true;
    }

    private void LateUpdate()
    {
        if (closeCombatEnemy.isDead)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - flightHeightOffset, transform.position.z);
            Destroy(GetComponent<FixWatcherPosition>());
        }
        if (pauseManager.pause && changePositionDuringPause)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + flightHeightOffset, transform.position.z);
            changePositionDuringPause = false;
        }
        if (!pauseManager.pause)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + flightHeightOffset, transform.position.z);
        }
        if (!pauseManager.pause && !changePositionDuringPause)
        {
            changePositionDuringPause = true;
        }
    }
}