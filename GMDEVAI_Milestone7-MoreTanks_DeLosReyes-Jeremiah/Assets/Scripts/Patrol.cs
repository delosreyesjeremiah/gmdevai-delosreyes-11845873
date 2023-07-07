using UnityEngine;

public class Patrol : NPCBaseFSM
{
    private GameObject[] _waypoints;
    private int _currentWaypoint;

    private void Awake()
    {
        _waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _currentWaypoint = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_waypoints.Length == 0) return;

        if (Vector3.Distance(_waypoints[_currentWaypoint].transform.position, NPC.transform.position) < Accuracy)
        {
            _currentWaypoint++;
            if (_currentWaypoint >= _waypoints.Length)
            {
                _currentWaypoint = 0;
            }
        }

        MoveTowardsTarget(_waypoints[_currentWaypoint]);
    }
}
