using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float verticalRotationSpeed = 100f;
    public float minXRotation = -45f;
    public float maxXRotation = 45f;

    private float horizontalInput;
    private float verticalInput;
    private float currentXRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

        float mouseY = Input.GetAxis("Mouse Y");
        currentXRotation -= mouseY * verticalRotationSpeed * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation);

        Vector3 clampedRotation = new Vector3(currentXRotation, transform.eulerAngles.y, 0f);
        transform.eulerAngles = clampedRotation;
    }
}
