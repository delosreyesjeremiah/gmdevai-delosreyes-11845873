using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;              // The target destination the object will travel towards.
    public float movementSpeed;         // The speed at which the object moves towards the goal.
    public float rotationSpeed;         // The speed at which the object rotates towards the goal.
    public float accelerationSpeed;     // The speed at which the object accelerates/decelerates towards the goal.
    public float stoppingDistance;      // The distance at which the object stops moving towards the goal.

    // Late Update is called once per frame
    private void LateUpdate()
    {
        // Calculate the position to look at by keeping the same Y position as the object itself.
        Vector3 lookAtGoal = new Vector3(   goal.position.x,
                                            this.transform.position.y,
                                            goal.position.z
                                         );

        // Calculate the direction from the current position to the target position.
        Vector3 direction = lookAtGoal - this.transform.position;

        // Rotate the object smoothly towards the target direction using Slerp.
        this.transform.rotation = Quaternion.Slerp( 
                                                    this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    rotationSpeed * Time.deltaTime
                                                  );

        // Make the object continously face towards the target position.
        this.transform.LookAt(lookAtGoal);

        // Check if the object has not reached the stopping distance from the target position.
        if (Vector3.Distance(lookAtGoal, this.transform.position) > stoppingDistance)
        {
            // Calculate the acceleration
            float acceleration = accelerationSpeed * Time.deltaTime;

            // Interpolate the current position towards the target position
            this.transform.position = Vector3.Lerp(this.transform.position, lookAtGoal, acceleration);

            // Move the object forward based on its movement speed.
            this.transform.Translate(0.0f, 0.0f, movementSpeed * Time.deltaTime);
        }
    }
}
