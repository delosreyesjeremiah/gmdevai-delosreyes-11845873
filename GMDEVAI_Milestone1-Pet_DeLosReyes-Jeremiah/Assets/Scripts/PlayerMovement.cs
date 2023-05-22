using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;     // Adjust this value to change the movement speed
    public float rotationSpeed;     // Adjust this value to change the rotation speed

    // Late Update is called once per frame
    void LateUpdate()
    {
        // Get the horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the inputs
        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // Move the object in the calculated direction
        this.transform.Translate(movementDirection * movementSpeed * Time.deltaTime);

        // Get the mouse movement input
        float mouseXInput = Input.GetAxis("Mouse X");

        // Rotate the object based on the mouse movement input
        this.transform.Rotate(Vector3.up, mouseXInput * rotationSpeed * Time.deltaTime);
    }
}
