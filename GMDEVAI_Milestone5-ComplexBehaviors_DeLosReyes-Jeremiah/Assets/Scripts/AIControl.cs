using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private enum AgentBehavior
    {
        Pursue,
        Hide,
        Evade
    }

    [SerializeField] private AgentBehavior _agentBehavior;
    private const float _detectionRadius = 15.0f;

    private NavMeshAgent _agent;
    private Vector3 _wanderTarget;
    private GameObject[] _hidingSpots;
    private Collider[] _hidingSpotColliders;

    [SerializeField] private GameObject _target;
    private WASDMovement _playerMovement;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerMovement = _target.GetComponent<WASDMovement>();

        _hidingSpots = World.Instance.HidingSpots;
        _hidingSpotColliders = new Collider[_hidingSpots.Length];
        for (int i = 0; i < _hidingSpotColliders.Length; i++)
        {
            _hidingSpotColliders[i] = _hidingSpots[i].GetComponent<Collider>();
        }
    }

    private void Update()
    {
        UpdateAgentBehavior();
    }

    private bool InRange()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _detectionRadius)
        {
            return true;
        }

        return false;
    }

    private void UpdateAgentBehavior()
    {
        if (InRange())
        {
            switch (_agentBehavior)
            {
                case AgentBehavior.Pursue:
                    Pursue();
                    break;
                case AgentBehavior.Hide:
                    if (CanSeeTarget())
                        Hide();
                    else
                        Wander();
                    break;
                case AgentBehavior.Evade:
                    Evade();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Wander();
        }
    }

    private void Seek(Vector3 location)
    {
        _agent.SetDestination(location);
    }

    private void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - transform.position;
        _agent.SetDestination(transform.position - fleeDirection);
    }

    private Vector3 CalculatePursueOrEvadeOffset()
    {
        Vector3 targetDirection = _target.transform.position - transform.position;
        float lookAhead = targetDirection.magnitude / (_agent.speed + _playerMovement.CurrentSpeed);
        return _target.transform.forward * lookAhead;
    }

    private void Pursue()
    {
        Vector3 offset = CalculatePursueOrEvadeOffset();
        Seek(_target.transform.position + offset);
    }

    private void Evade()
    {
        Vector3 offset = CalculatePursueOrEvadeOffset();
        Flee(_target.transform.position + offset);
    }

    private void Wander()
    {
        const float wanderRadius = 20.0f;
        const float wanderDistance = 10.0f;
        const float wanderJitter = 1.0f;

        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)) * wanderJitter;
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector3 target = transform.InverseTransformVector(_wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance));

        Seek(target);
    }

    private void Hide()
    {
        float hideOffset = 5.0f;
        float distance = Mathf.Infinity;

        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;

        Collider chosenCollider = null;

        int hidingSpotsCount = World.Instance.HidingSpots.Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            GameObject hidingSpot = _hidingSpots[i];
            Collider hidingSpotCollider = _hidingSpotColliders[i];

            Vector3 hideDirection = hidingSpot.transform.position - _target.transform.position;
            Vector3 hidePosition = hidingSpot.transform.position + hideDirection.normalized * hideOffset;

            float spotDistance = Vector3.Distance(transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenCollider = hidingSpotCollider;
                distance = spotDistance;
            }
        }

        if (chosenCollider != null) 
        {
            Ray ray = new Ray(chosenSpot, -chosenDirection.normalized);
            float rayDistance = 100.0f;

            if (chosenCollider.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                Vector3 hideDestination = hit.point + chosenDirection.normalized * hideOffset;
                Seek(hideDestination);
            }
        }
    }

    private bool CanSeeTarget()
    {
        Vector3 rayToTarget = _target.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayToTarget, out RaycastHit hit))
        {
            return hit.transform.gameObject.CompareTag("Player");
        }

        return false;
    }
}
