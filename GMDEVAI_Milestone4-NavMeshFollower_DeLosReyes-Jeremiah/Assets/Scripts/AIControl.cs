using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private NavMeshAgent _agent;

    public NavMeshAgent Agent
    {
        get { return _agent; }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
}
