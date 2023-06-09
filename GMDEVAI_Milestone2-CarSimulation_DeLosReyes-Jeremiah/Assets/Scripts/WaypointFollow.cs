using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Utility.WaypointCircuit _circuit;
    private int _currentWayPointIndex = 0;

    [SerializeField] private float _movementSpeed = 5.0f;
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private float _accuracy = 1.0f;

    private void LateUpdate()
    {
        if (_circuit.Waypoints.Length == 0) return;

        GameObject currentWayPoint = _circuit.Waypoints[_currentWayPointIndex].gameObject;

        Vector3 lookAtGoal = new Vector3(currentWayPoint.transform.position.x, transform.position.y, currentWayPoint.transform.position.z);

        Vector3 direction = lookAtGoal - transform.position;

        if (direction.magnitude < _accuracy)
        {
            _currentWayPointIndex++;
            if (_currentWayPointIndex >= _circuit.Waypoints.Length)
            {
                _currentWayPointIndex = 0;
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

        transform.Translate(0.0f, 0.0f, _movementSpeed * Time.deltaTime);
    }
}
