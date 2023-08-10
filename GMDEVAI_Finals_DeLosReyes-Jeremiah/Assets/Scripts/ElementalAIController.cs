using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ElementalAIController : MonoBehaviour
{
    #region Fields

    [SerializeField] private List<Transform> _flockDestinationPoints;
    [SerializeField] private float _minimumFlockDistance;
    [SerializeField] private float _stationaryDurationToReturnToFlock;
    [SerializeField] private float _rotationSpeed;

    private NavMeshAgent _agent;
    private ElementalAI _elementalAI;

    private float _chaseStartTime;

    #endregion

    #region Unity Messages

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _elementalAI = GetComponent<ElementalAI>();
    }

    private void Start()
    {
        SetRandomFlockDestination();
    }

    private void LateUpdate()
    {
        switch (_elementalAI.ElementalAIState)
        {
            case ElementalAIState.Flock:
                {
                    Flock();
                    break;
                }
            case ElementalAIState.Chase:
                {
                    if (IsStationaryForDuration(_stationaryDurationToReturnToFlock))
                    {
                        _elementalAI.ElementalAIState = ElementalAIState.Flock;
                    }
                    else
                    {
                        Chase(_elementalAI.Target);
                    }

                    break;
                }
            case ElementalAIState.Attack:
                {
                    Attack(_elementalAI.Target);
                    break;
                }
        }
    }

    #endregion

    #region Private Methods

    private void Flock()
    {
        if (_agent.remainingDistance < _minimumFlockDistance)
        {
            SetRandomFlockDestination();
        }
    }

    private void Chase(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    private void Attack(Transform target)
    {
        RotateTowards(target);

        if (!_elementalAI.IsShooting)
        {
            _elementalAI.StartShooting();
        }
    }

    private void SetRandomFlockDestination()
    {
        int randomIndex = Random.Range(0, _flockDestinationPoints.Count);
        _agent.SetDestination(_flockDestinationPoints[randomIndex].position);
    }

    private void RotateTowards(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    private bool IsStationaryForDuration(float duration)
    {
        if (_agent.velocity.sqrMagnitude <= 1.0f)
        {
            if (Time.time - _chaseStartTime >= duration)
            {
                return true;
            }
        }
        else
        {
            _chaseStartTime = Time.time;
        }

        return false;
    }

    #endregion
}
