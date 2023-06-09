using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daredevil : RaceCar
{
    private void Awake()
    {
        MovementSpeed = 30.0f;
        BrakeAngle = 15.0f;
    }
}
