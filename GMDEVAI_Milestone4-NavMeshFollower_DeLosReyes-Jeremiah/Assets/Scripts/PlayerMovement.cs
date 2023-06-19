using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public delegate void PlayerMoveHandler(Vector3 newPosition);
    public static event PlayerMoveHandler OnPlayerMove;

    private const float _movementSpeed = 5.0f; // Adjust this value to change the movement speed
    private const float _rotationSpeed = 300.0f; // Adjust this value to change the rotation speed

    private Vector3 _movementDirection;
    private Vector3 _currentPosition;

    private void Start()
    {
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position != _currentPosition)
        {
            _currentPosition = transform.position;
            OnPlayerMove?.Invoke(_currentPosition);
        }
    }

    private void LateUpdate()
    {
        ProcessMovementInput();
        ProcessRotationInput();
    }

    private void ProcessMovementInput()
    {
        // Get the horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the inputs
        _movementDirection.Set(horizontalInput, 0.0f, verticalInput);
        _movementDirection.Normalize();

        // Move the object in the calculated direction
        transform.Translate(_movementDirection * (_movementSpeed * Time.deltaTime));
    }

    private void ProcessRotationInput()
    {
        // Get the mouse movement input
        float mouseXInput = Input.GetAxis("Mouse X");

        // Rotate the object based on the mouse movement input
        transform.Rotate(Vector3.up, mouseXInput * _rotationSpeed * Time.deltaTime);
    }
}
