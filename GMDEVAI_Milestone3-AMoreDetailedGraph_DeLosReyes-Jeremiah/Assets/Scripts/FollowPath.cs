using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private WaypointManager _waypointManager;
    private Graph _graph;
    private GameObject[] _waypoints;
    private GameObject _currentNode;
    private int _currentWaypointIndex = 0;

    private Transform _goal;
    private const float _speed = 5.0f;
    private const float _rotationSpeed = 2.0f;
    private const float _accuracy = 1.0f;

    public void GoToBarracks()
    {
        if (_currentNode == _waypoints[6])
            return;

        _graph.AStar(_currentNode, _waypoints[6]);
        _currentWaypointIndex = 0;
    }

    public void GoToRadar()
    {
        if (_currentNode == _waypoints[3])
            return;

        _graph.AStar(_currentNode, _waypoints[3]);
        _currentWaypointIndex = 0;
    }

    public void GoToTwinMountains()
    {
        if (_currentNode == _waypoints[7])
            return;

        _graph.AStar(_currentNode, _waypoints[7]);
        _currentWaypointIndex = 0;
    }

    public void GoToCommandPost()
    {
        if (_currentNode == _waypoints[4])
            return;

        _graph.AStar(_currentNode, _waypoints[4]);
        _currentWaypointIndex = 0;
    }

    public void GoToRuins()
    {
        if (_currentNode == _waypoints[17])
            return;

        _graph.AStar(_currentNode, _waypoints[17]);
        _currentWaypointIndex = 0;
    }

    public void GoToOilRefinery()
    {
        if (_currentNode == _waypoints[10])
            return;

        _graph.AStar(_currentNode, _waypoints[10]);
        _currentWaypointIndex = 0;
    }

    public void GoToTankers()
    {
        if (_currentNode == _waypoints[12])
            return;

        _graph.AStar(_currentNode, _waypoints[12]);
        _currentWaypointIndex = 0;
    }

    public void GoToCommandCenter()
    {
        if (_currentNode == _waypoints[15])
            return;

        _graph.AStar(_currentNode, _waypoints[15]);
        _currentWaypointIndex = 0;
    }

    public void GoToMiddleOfMap()
    {
        if (_currentNode == _waypoints[9])
            return;

        _graph.AStar(_currentNode, _waypoints[9]);
        _currentWaypointIndex = 0;
    }

    private void Start()
    {
        _waypoints = _waypointManager.GetWaypoints();
        _graph = _waypointManager.GetGraph();
        _currentNode = _waypoints[8];
    }

    private void LateUpdate()
    {
        if (_graph.getPathLength() == 0 || _currentWaypointIndex == _graph.getPathLength())
        {
            return;
        }

        _currentNode = _graph.getPathPoint(_currentWaypointIndex);
        Vector3 waypointPosition = _currentNode.transform.position;

        if (Vector3.Distance(waypointPosition, transform.position) < _accuracy)
        {
            _currentWaypointIndex++;
        }

        if (_currentWaypointIndex < _graph.getPathLength())
        {
            _goal = _graph.getPathPoint(_currentWaypointIndex).transform;

            Vector3 lookAtGoal = _goal.position;
            lookAtGoal.y = transform.position.y;

            Vector3 direction = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
