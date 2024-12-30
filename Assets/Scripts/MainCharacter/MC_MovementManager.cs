using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MC_MovementManager : MonoBehaviour
{
    private float rotationY;
    [SerializeField] private Transform head;
    private CharacterController characterController;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private MC_LevelManager levelManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_SatietyManager satietyManager;
    private bool isJumping = false;
    private int currentJumpNumber;
    private Coroutine jumpCoroutine;
    private Vector3 dashDirection;
    private float currentDashDuration;
    private float currentDashRechargeTime;
    private bool isDashing = false;
    private float currentTeleportationRechargeTime;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        currentDashRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
        currentTeleportationRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
    }

    // Delete this(To Dmytro):

    private float speed = 5;
    public float Speed
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

    // Stop deleting.

    private void Update()
    {
        satietyManager.canReplenishEnergy = true;

        // Rotation

        if (!pauseManager.pause)
        {
            // must be multiplied by sensetivity.
            gameObject.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * 10);
            rotationY += Input.GetAxis("Mouse Y") * 10f;
            rotationY = Mathf.Clamp(rotationY, -80, 80);
            head.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }

        // Walking

        if (!isDashing && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                characterController.Move(transform.forward * statisticsInfo.WalkingSpeedValue * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                characterController.Move(-transform.right * statisticsInfo.WalkingSpeedValue * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                characterController.Move(-transform.forward * statisticsInfo.WalkingSpeedValue * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                characterController.Move(transform.right * statisticsInfo.WalkingSpeedValue * Time.deltaTime);
            }
        }

        // Running

        if (!isDashing && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && energyManager.Energy >= statisticsInfo.RunningEnergySpendingMultiplier * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime)
        {
            if (Input.GetKey(KeyCode.W))
            {
                characterController.Move(transform.forward * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                characterController.Move(-transform.right * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                characterController.Move(-transform.forward * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                characterController.Move(transform.right * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
            }
            if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) && !(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) && !(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
            {            
                energyManager.SpendEnergy(statisticsInfo.RunningEnergySpendingMultiplier * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
                satietyManager.canReplenishEnergy = false;
            }
        }

        // Gravity

        if (!characterController.isGrounded && !isJumping && !isDashing)
        {
            characterController.Move(-transform.up * statisticsInfo.GravityValue * Time.deltaTime);
        }

        // Jumping

        if (characterController.isGrounded)
        {
            currentJumpNumber = 0;
        }
        if (!isDashing && ((Input.GetKey(KeyCode.Space) && characterController.isGrounded && energyManager.Energy >= statisticsInfo.JumpingEnergySpendingMultiplier) || (Input.GetKeyDown(KeyCode.Space) && energyManager.Energy >= statisticsInfo.JumpingEnergySpendingMultiplier * Mathf.Pow(2, currentJumpNumber))))
        {
            if (!characterController.isGrounded)
            {
                currentJumpNumber++;
            }
            if (currentJumpNumber <= statisticsInfo.AdditionalJumpNumberValues[statisticsManager.MovementLevel])
            {
                if (jumpCoroutine != null)
                {
                    StopCoroutine(jumpCoroutine);
                }
                jumpCoroutine = StartCoroutine(Jump());
                energyManager.SpendEnergy(statisticsInfo.JumpingEnergySpendingMultiplier * Mathf.Pow(2, currentJumpNumber));
                satietyManager.canReplenishEnergy = false;
            }
        }

        // Dashing

        if (statisticsManager.MovementLevel >= statisticsInfo.DashUnlockLevel)
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentDashRechargeTime <= 0f)
            {
                isDashing = true;
                dashDirection = transform.forward;
            }
            if (isDashing)
            {
                if (energyManager.Energy >= statisticsInfo.DashingEnergySpendingMultiplier * statisticsInfo.DashingSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime)
                {
                    characterController.Move(dashDirection * statisticsInfo.DashingSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
                    energyManager.SpendEnergy(statisticsInfo.DashingEnergySpendingMultiplier * statisticsInfo.DashingSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
                    satietyManager.canReplenishEnergy = false;
                    currentDashDuration += Time.deltaTime;
                }
                else
                {
                    isDashing = false;
                    currentDashDuration = 0f;
                    currentDashRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
                }
            }
            else
            {
                currentDashRechargeTime -= Time.deltaTime;
            }
            if (currentDashDuration >= statisticsInfo.DashingDurationValue)
            {
                isDashing = false;
                currentDashDuration = 0f;
                currentDashRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
            }
        }

        // Teleportation

        if (Input.GetKeyDown(KeyCode.Tab) && statisticsManager.MovementLevel >= statisticsInfo.TeleportationUnlockLevel && currentTeleportationRechargeTime <= 0)
        {
            Ray ray = new Ray(head.transform.position, head.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (statisticsInfo.TeleportationMaxDistanceValues[statisticsManager.MovementLevel] >= hit.distance && energyManager.Energy >= statisticsInfo.TeleportationEnergySpendingMultiplier * Vector3.Distance(transform.position, hit.point))
                {
                    energyManager.SpendEnergy(statisticsInfo.TeleportationEnergySpendingMultiplier * Vector3.Distance(transform.position, hit.point));
                    characterController.Move(hit.point - transform.position);
                    satietyManager.canReplenishEnergy = false;
                    currentTeleportationRechargeTime = statisticsInfo.TeleportationRechargeTimeValues[statisticsManager.MovementLevel];
                }
            }
        }
        currentTeleportationRechargeTime -= Time.deltaTime;
    }

    private IEnumerator Jump()
    {
        isJumping = true;
        float progress = 0f;
        while (progress < 0.5f)
        {
            progress += Time.deltaTime / statisticsInfo.JumpDuration;
            characterController.Move(Vector3.Lerp(Vector3.zero, new Vector3(0, statisticsInfo.JumpHeight, 0), statisticsInfo.JumpCurve.Evaluate(progress / (statisticsInfo.JumpHeight * statisticsInfo.JumpDuration))));
            yield return null;
        }
        yield return new WaitForSeconds(statisticsInfo.TimeBeforeFallingDuringJump);
        while (progress < 1f)
        {
            progress += Time.deltaTime / statisticsInfo.JumpDuration;
            characterController.Move(Vector3.Lerp(Vector3.zero, new Vector3(0, -statisticsInfo.JumpHeight, 0), statisticsInfo.JumpCurve.Evaluate(progress / (statisticsInfo.JumpHeight * statisticsInfo.JumpDuration))));
            yield return null;
        }
        isJumping = false;
    }
}