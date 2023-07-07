using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private List<GameObject> _goalLocations;
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _speedMultiplier;
    private readonly float _detectionRadius = 10.0f;
    private readonly float _fleeRadius = 10.0f;
    private readonly float _minimumFlockDistance = 3.0f;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _goalLocations = new List<GameObject>(GameObject.FindGameObjectsWithTag("goal"));

        SetWalkOffset();
        ResetAgent();
        SetRandomDestination();
    }

    private void LateUpdate()
    {
        if (_agent.remainingDistance < _minimumFlockDistance)
        {
            ResetAgent();
            SetRandomDestination();
        }
    }

    public void DetectNewObstacle(Vector3 location)
    {
        if (Vector3.Distance(location, transform.position) < _detectionRadius)
        {
            Vector3 fleeDirection = (transform.position - location).normalized;
            Vector3 newGoal = transform.position + (fleeDirection * _fleeRadius);

            NavMeshPath path = new NavMeshPath();
            if (_agent.CalculatePath(newGoal, path) && path.status != NavMeshPathStatus.PathInvalid)
            {
                _agent.SetDestination(path.corners[path.corners.Length - 1]);
                SetRunningState();
            }
        }
    }

    public void GoToNewGoal(GameObject goal)
    {
        _goalLocations.Add(goal);
        _agent.SetDestination(goal.transform.position);
    }

    private void ResetAgent()
    {
        _speedMultiplier = Random.Range(0.1f, 1.5f);
        _agent.speed = 2.0f * _speedMultiplier;
        _agent.angularSpeed = 120.0f;

        _animator.SetFloat("speedMultiplier", _speedMultiplier);
        _animator.SetTrigger("isWalking");

        _agent.ResetPath();
    }

    private void SetRandomDestination()
    {
        int randomIndex = Random.Range(0, _goalLocations.Count);
        _agent.SetDestination(_goalLocations[randomIndex].transform.position);
    }

    private void SetWalkOffset()
    {
        float walkOffset = Random.Range(0.1f, 1.0f);
        _animator.SetFloat("walkOffset", walkOffset);
    }

    private void SetRunningState()
    {
        _animator.SetTrigger("isRunning");
        _agent.speed = 10.0f;
        _agent.angularSpeed = 500.0f;
    }
}