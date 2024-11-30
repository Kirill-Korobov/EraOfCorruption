using UnityEngine;

public class MC_MovementManager : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private Transform head;
    private float rotationY;
    private const float speedMultiplier = 5;
    private const float gravity = 9.8f;
    private float velocity;
    private float speed = 5;
    private float jumpSpeed = 10f;
    private float maxJumpDuration = 1f;
    private int maxJumpNumber = 5;
    private float currentJumpDuration;
    private int currentJumpNumber;
    private bool isJumping = false;
    private bool fallDuringJump;
    private Vector3 dashDirection;
    private float dashSpeed = 50f;
    private float dashDuration = 0.2f;
    private float currentDashDuration;
    private bool isDashing = false;  

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        // Set movement stats.
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

    private void Update()
    {
        // Rotation

        // if not pause.
        if (Input.GetMouseButton(1))
        {
            // must be multiplied by sensetivity.
            gameObject.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * 10);
            rotationY += Input.GetAxis("Mouse Y") * 10f;
            rotationY = Mathf.Clamp(rotationY, -80, 80);
            head.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }

        // Walking

        if (!isDashing && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.W))
            {
                characterController.Move(transform.forward * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
            if (Input.GetKey(KeyCode.A))
            {
                characterController.Move(-transform.right * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
            if (Input.GetKey(KeyCode.S))
            {
                characterController.Move(-transform.forward * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
            if (Input.GetKey(KeyCode.D))
            {
                characterController.Move(transform.right * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
        }

        // Running

        if (!isDashing && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl)))
        {
            if (Input.GetKey(KeyCode.W))
            {
                characterController.Move(transform.forward * speed * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
            if (Input.GetKey(KeyCode.A))
            {
                characterController.Move(-transform.right * speed * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
            if (Input.GetKey(KeyCode.S))
            {
                characterController.Move(-transform.forward * speed * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
            if (Input.GetKey(KeyCode.D))
            {
                characterController.Move(transform.right * speed * Time.deltaTime * speedMultiplier);
                // minus energy.
            }
        }

        // Gravity
       
        
        if ((!characterController.isGrounded && !isJumping && !isDashing) || fallDuringJump)
        {
            velocity = -gravity * Time.deltaTime;
            characterController.Move(transform.up * velocity);
        }

        // Jumping

        if (!isDashing)
        {
            if (characterController.isGrounded)
            {
                isJumping = false;
                currentJumpDuration = 0f;
                currentJumpNumber = 0;
                fallDuringJump = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded && !isJumping)
            {
                isJumping = true;
                currentJumpDuration = 0f;
                currentJumpNumber = 1;
                fallDuringJump = false;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isJumping && currentJumpNumber < maxJumpNumber)
            {
                currentJumpDuration = 0f;
                currentJumpNumber++;
                fallDuringJump = false;
            }
            if (isJumping && !fallDuringJump)
            {
                characterController.Move(transform.up * jumpSpeed * Time.deltaTime);
                currentJumpDuration += Time.deltaTime;
            }
            if (currentJumpDuration >= maxJumpDuration || !Input.GetKey(KeyCode.Space))
            {
                fallDuringJump = true;
            }
        }

        // Dash

        if (Input.GetKeyDown(KeyCode.E))
        {
            isDashing = true;
            currentDashDuration = 0f;
            dashDirection = transform.forward;
        }
        if (isDashing)
        {
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
            currentDashDuration += Time.deltaTime;
        }
        if (currentDashDuration >= dashDuration)
        {
            isDashing = false;
        }
    }
}