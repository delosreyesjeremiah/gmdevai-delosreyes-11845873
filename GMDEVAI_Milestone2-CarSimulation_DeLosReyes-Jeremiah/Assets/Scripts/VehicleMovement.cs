using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private Transform _goal;
    private RaceCar _raceCar;
    
    private float _speed;
    private float _rotationSpeed;
    private float _acceleration;
    private float _deceleration;
    private float _minimumSpeed;
    private float _maximumSpeed;
    private float _brakeAngle;
    private float _decelerationThreshold;

    private void Awake()
    {
        _raceCar = GetComponent<RaceCar>();
    }

    private void Start()
    {
        float halfSpeed = _raceCar.MovementSpeed * 0.5f;

        // Initialization of variables
        _speed = 0.0f;
        _rotationSpeed = _raceCar.MovementSpeed;
        _acceleration = halfSpeed;
        _deceleration = halfSpeed + (halfSpeed * 0.5f);
        _minimumSpeed = 0.0f;
        _maximumSpeed = _raceCar.MovementSpeed;
        _brakeAngle = _raceCar.BrakeAngle;
        _decelerationThreshold = halfSpeed;
    }

    private void LateUpdate()
    {
        // Get the position of the goal, and set the y-coordinate to match the current y-coordinate of the vehicle
        Vector3 lookAtGoal = _goal.position;
        lookAtGoal.y = transform.position.y;

        // Calculate the direction towards the goal
        Vector3 direction = lookAtGoal - transform.position;

        // Rotate the vehicle gradually towards the goal direction based on rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

        // If the angle between the goal's forward direction and the vehicle's forward direction is less than the brake angle
        // and the speed is greater than the deceleration threshold
        if (Vector3.Angle(_goal.forward, transform.forward) < _brakeAngle && _speed > _decelerationThreshold)
        {
            // Decelerate the vehicle
            _speed = Mathf.Clamp(_speed - (_deceleration * Time.deltaTime), _minimumSpeed, _maximumSpeed);
        }
        else
        {
            // Accelerate the vehicle
            _speed = Mathf.Clamp(_speed + (_acceleration * Time.deltaTime), _minimumSpeed, _maximumSpeed);
        }

        // Move the vehicle forward based on the current speed
        transform.Translate(Vector3.forward * _speed);
    }
}
