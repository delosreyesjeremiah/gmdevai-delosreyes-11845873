using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private Transform _goal;
    private AIControl[] _aiControls;

    private void Awake()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("AI");
        _aiControls = new AIControl[agents.Length];

        int index = 0;
        foreach (GameObject ai in agents)
        {
            _aiControls[index] = ai.GetComponent<AIControl>();
            index++;
        }

        PlayerMovement.OnPlayerMove += UpdateAgentDestinations;
    }

    private void Start()
    {
        UpdateAgentDestinations(_goal.position);
    }

    private void OnDestroy()
    {
        PlayerMovement.OnPlayerMove -= UpdateAgentDestinations;
    }

    private void UpdateAgentDestinations(Vector3 newPosition)
    {
        foreach (AIControl ai in _aiControls)
        {
            ai.Agent.SetDestination(newPosition);
        }
    }
}
