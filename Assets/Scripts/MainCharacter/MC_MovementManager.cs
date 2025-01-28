using System.Collections;
using UnityEngine;

public class MC_MovementManager : MonoBehaviour
{
    [SerializeField] private Transform head;
    private CharacterController characterController;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private StatisticsInfo statisticsInfo;
    [SerializeField] private MC_StatisticsManager statisticsManager;
    [SerializeField] private MC_LevelManager levelManager;
    [SerializeField] private MC_EnergyManager energyManager;
    [SerializeField] private MC_SatietyManager satietyManager;
    [SerializeField] private MC_PerspectiveManager perspectiveManager;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform modelTransform;
    private bool isJumping = false;
    private int currentJumpNumber;
    private Coroutine jumpCoroutine;
    private Vector3 dashDirection;
    private float currentDashDuration;
    private float currentDashRechargeTime;
    private bool isDashing = false;
    private float currentTeleportationRechargeTime;

    private void Start()
    {
        // Set main character`s position.
        characterController = GetComponent<CharacterController>();
        currentDashRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
        currentTeleportationRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
        walkSpeed = 1f;
    }

    // Stop deleting.
    [HideInInspector] public float walkSpeed = 1f;
    private void Update()
    {
        if (!pauseManager.pause)
        {
            satietyManager.canReplenishEnergy = true;

            // Model rotation

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                modelTransform.localEulerAngles = Vector3.zero;
            }
            else
            {
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
                {
                    modelTransform.localEulerAngles = new Vector3(0, 45, 0);
                }
                else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
                {
                    modelTransform.localEulerAngles = new Vector3(0, 135, 0);
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
                {
                    modelTransform.localEulerAngles = new Vector3(0, 225, 0);
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                {
                    modelTransform.localEulerAngles = new Vector3(0, 315, 0);
                }
                else
                {
                    if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                    {
                        modelTransform.localEulerAngles = Vector3.zero;
                    }
                    else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                    {
                        modelTransform.localEulerAngles = new Vector3(0, 90, 0);
                    }
                    else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                    {
                        modelTransform.localEulerAngles = new Vector3(0, 180, 0);
                    }
                    else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    {
                        modelTransform.localEulerAngles = new Vector3(0, 270, 0);
                    }
                }
            }

            bool isWalkingOrRunning = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) && !(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) && !(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D));

            // Walking

            if (!isDashing && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    characterController.Move(transform.forward * statisticsInfo.WalkingSpeedValue * Time.deltaTime * walkSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    characterController.Move(-transform.right * statisticsInfo.WalkingSpeedValue * Time.deltaTime * walkSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    characterController.Move(-transform.forward * statisticsInfo.WalkingSpeedValue * Time.deltaTime * walkSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    characterController.Move(transform.right * statisticsInfo.WalkingSpeedValue * Time.deltaTime * walkSpeed);
                }
                if (isWalkingOrRunning)
                {
                    animator.SetBool("Walk", true);
                }
                else
                {
                    animator.SetBool("Walk", false);
                }
            }
            else
            {
                animator.SetBool("Walk", false);
            }

            // Running

            if (!isDashing && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && energyManager.Energy >= statisticsInfo.RunningEnergySpendingMultiplier * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    characterController.Move(transform.forward * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime * walkSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    characterController.Move(-transform.right * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime * walkSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    characterController.Move(-transform.forward * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime * walkSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    characterController.Move(transform.right * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime * walkSpeed);
                }
                if (isWalkingOrRunning)
                {
                    animator.SetBool("Run", true);
                    energyManager.SpendEnergy(statisticsInfo.RunningEnergySpendingMultiplier * statisticsInfo.RunningSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
                    satietyManager.canReplenishEnergy = false;
                }
                else
                {
                    animator.SetBool("Run", false);
                }
            }
            else
            {
                animator.SetBool("Run", false);
            }

            // Fall

            if (!characterController.isGrounded && !isJumping && !isDashing)
            {
                characterController.Move(-transform.up * statisticsInfo.GravityValue * Time.deltaTime);

                Ray ray = new Ray(transform.position, -transform.up);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.distance >= 1.2f)
                    {
                        animator.SetBool("Fall", true);
                    }
                    else
                    {
                        animator.SetBool("Fall", false);
                    }
                }
            }

            // Jumping

            if (characterController.isGrounded)
            {
                currentJumpNumber = 0;
            }
            if (!isDashing && ((Input.GetKey(KeyCode.Space) && characterController.isGrounded && energyManager.Energy >= statisticsInfo.JumpingEnergySpendingMultiplier) || (Input.GetKeyDown(KeyCode.Space) && energyManager.Energy >= statisticsInfo.JumpingEnergySpendingMultiplier * Mathf.Pow(statisticsInfo.EveryNextAdditionalJumpEnergySpendingMultiplier, currentJumpNumber))))
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
                    energyManager.SpendEnergy(statisticsInfo.JumpingEnergySpendingMultiplier * Mathf.Pow(statisticsInfo.EveryNextAdditionalJumpEnergySpendingMultiplier, currentJumpNumber));
                    satietyManager.canReplenishEnergy = false;
                }
            }

            // Dashing

            if (statisticsManager.MovementLevel >= statisticsInfo.DashUnlockLevel)
            {
                if (Input.GetKeyDown(KeyCode.Q) && currentDashRechargeTime <= 0f)
                {
                    isDashing = true;
                    animator.SetBool("Dash", true);
                    if (perspectiveManager.CurrentPerspective == 1)
                    {
                        dashDirection = transform.forward;
                    }
                    else
                    {
                        dashDirection = modelTransform.forward;
                    }
                }
                if (isDashing)
                {
                    if (energyManager.Energy >= statisticsInfo.DashingEnergySpendingMultiplier * statisticsInfo.DashingSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime)
                    {
                        characterController.Move(dashDirection * statisticsInfo.DashingSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime * walkSpeed);
                        energyManager.SpendEnergy(statisticsInfo.DashingEnergySpendingMultiplier * statisticsInfo.DashingSpeedMultiplierValues[statisticsManager.MovementLevel] * Time.deltaTime);
                        satietyManager.canReplenishEnergy = false;
                        currentDashDuration += Time.deltaTime;
                    }
                    else
                    {
                        isDashing = false;
                        animator.SetBool("Dash", false);
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
                    animator.SetBool("Dash", false);
                    currentDashDuration = 0f;
                    currentDashRechargeTime = statisticsInfo.DashingRechargeTimeValues[statisticsManager.MovementLevel];
                }
            }

            // Teleportation

            if (Input.GetKeyDown(KeyCode.Tab) && statisticsManager.MovementLevel >= statisticsInfo.TeleportationUnlockLevel && currentTeleportationRechargeTime <= 0 && perspectiveManager.CurrentPerspective == 1)
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
    }

    private IEnumerator Jump()
    {
        isJumping = true;
        animator.SetBool("Jump", true);
        float progress = 0f;
        while (progress < 1f && !pauseManager.pause)
        {
            progress += Time.deltaTime / statisticsInfo.JumpDuration;
            characterController.Move(Vector3.Lerp(Vector3.zero, new Vector3(0, statisticsInfo.JumpHeight, 0), statisticsInfo.JumpCurve.Evaluate(progress / (statisticsInfo.JumpHeight * statisticsInfo.JumpDuration))));
            yield return null;
        }
        isJumping = false;
        animator.SetBool("Fall", true);
        animator.SetBool("Jump", false);
    }
}