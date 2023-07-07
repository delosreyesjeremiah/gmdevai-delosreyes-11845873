using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private GameObject _goalPrefab;
    private GameObject[] _agents;
    private Dictionary<GameObject, AIControl> _agentDictionary;
    private Camera _camera;

    private void Awake()
    {
        _agents = GameObject.FindGameObjectsWithTag("agent");

        _agentDictionary = new Dictionary<GameObject, AIControl>();
        foreach (GameObject agent in _agents)
        {
            AIControl aiControl = agent.GetComponent<AIControl>();
            _agentDictionary.Add(agent, aiControl);
        }

        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SpawnObject(_monsterPrefab);
        if (Input.GetMouseButtonDown(1)) SpawnObject(_goalPrefab);
    }

    private void SpawnObject(GameObject objectToSpawn)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit) && hit.collider.CompareTag("Ground"))
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, hit.point, objectToSpawn.transform.rotation);
            foreach (KeyValuePair<GameObject, AIControl> agent in _agentDictionary)
            {
                AIControl aiControl = agent.Value;

                if (objectToSpawn == _monsterPrefab)
                {
                    aiControl.DetectNewObstacle(spawnedObject.transform.position);
                }
                else if (objectToSpawn == _goalPrefab)
                {
                    aiControl.GoToNewGoal(spawnedObject);
                }
            }
        }
    }
}