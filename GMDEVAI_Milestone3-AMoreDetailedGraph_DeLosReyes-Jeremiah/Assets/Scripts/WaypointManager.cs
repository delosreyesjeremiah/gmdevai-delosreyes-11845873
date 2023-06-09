using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum Direction { UNI, BI };
    public Direction direction;
    public GameObject node1;
    public GameObject node2;
}

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _waypoints;
    [SerializeField] private Link[] _links;
    [SerializeField] private Graph _graph = new Graph();

    public GameObject[] GetWaypoints()
    {
        return _waypoints;
    }

    public Graph GetGraph()
    {
        return _graph;
    }

    private void Start()
    {
        if (_waypoints.Length > 0)
        {
            foreach (GameObject wp in _waypoints)
            {
                _graph.AddNode(wp);
            }

            foreach (Link l in _links)
            {
                _graph.AddEdge(l.node1, l.node2);
                if (l.direction == Link.Direction.BI)
                {
                    _graph.AddEdge(l.node2, l.node1);
                }
            }
        }
    }

    private void Update()
    {
        _graph.debugDraw();
    }
}
