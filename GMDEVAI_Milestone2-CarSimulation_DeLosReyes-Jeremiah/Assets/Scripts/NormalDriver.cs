using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDriver : RaceCar
{
    private void Awake()
    {
        MovementSpeed = 20.0f;
        BrakeAngle = 30.0f;
    }
}
