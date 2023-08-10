using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields

    private Player _player;
    private Vector3 _movementDirection;

    private float _shootTimer;

    #endregion

    #region Unity Messages

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_shootTimer <= 0.0f)
            {
                _player.Gun.Shoot();
                _shootTimer = _player.FireRate;
            }
        }

        if (_shootTimer > 0.0f)
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        ProcessMovementInput();
        ProcessRotationInput();
    }

    #endregion

    #region Private Methods

    private void ProcessMovementInput()
    {
        // Get the horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the inputs
        _movementDirection.Set(horizontalInput, 0.0f, verticalInput);
        _movementDirection.Normalize();

        // Move the object in the calculated direction
        transform.Translate(_movementDirection * (_player.MovementSpeed * Time.deltaTime));
    }

    private void ProcessRotationInput()
    {
        // Get the mouse movement input
        float mouseXInput = Input.GetAxis("Mouse X");

        // Rotate the object based on the mouse movement input
        transform.Rotate(Vector3.up, mouseXInput * _player.RotationSpeed * Time.deltaTime);
    }

    #endregion
}
