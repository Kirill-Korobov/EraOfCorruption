using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    public float verticalRotationSpeed = 100f;
    public float minXRotation = -45f;
    public float maxXRotation = 45f;

    private float horizontalInput;
    private float verticalInput;
    private float currentXRotation = 0f;

    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     verticalInput = Input.GetAxis("Vertical");

    //     Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
    //     transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

    //     // float mouseX = Input.GetAxis("Mouse X");
    //     // //camera.transform.Rotate(Vector3.up, mouseX * rotationSpeed);
    //     // camera.transform.rotation = Quaternion.Euler(0, mouseX * rotationSpeed, 0) * camera.transform.rotation;
        

    //     // float mouseY = Input.GetAxis("Mouse Y");
    //     // currentXRotation -= mouseY * verticalRotationSpeed;
    //     // currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation);
    //     // camera.transform.rotation = Quaternion.Euler(currentXRotation, 0, 0);

    //     currentXRotation -= Input.GetAxis("Mouse Y") * verticalRotationSpeed * Time.deltaTime;
    //     currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation);

    //     transform.rotation *= 
    //     Quaternion.AngleAxis( Time.deltaTime * verticalRotationSpeed * Input.GetAxis("Mouse X"), Vector3.up );

    //     transform.rotation *= 
    //     Quaternion.Euler(currentXRotation, 0, 0);

    // }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
