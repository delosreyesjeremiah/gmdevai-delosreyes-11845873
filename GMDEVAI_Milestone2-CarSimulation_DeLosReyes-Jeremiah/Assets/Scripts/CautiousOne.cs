using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautiousOne : RaceCar
{
    private void Awake()
    {
        MovementSpeed = 15.0f;
        BrakeAngle = 50.0f;
    }
}
