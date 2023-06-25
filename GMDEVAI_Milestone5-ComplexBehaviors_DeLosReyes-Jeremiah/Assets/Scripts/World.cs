using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World 
{
    private static readonly World _instance = new World();
    private static readonly GameObject[] _hidingSpots;

    static World()
    {
        _hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private World() {}

    public static World Instance
    {
        get { return _instance; }
    }

    public GameObject[] HidingSpots
    {
        get { return _hidingSpots;}
    }
}
