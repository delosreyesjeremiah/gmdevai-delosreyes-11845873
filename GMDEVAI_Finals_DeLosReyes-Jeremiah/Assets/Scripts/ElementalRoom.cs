using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalRoom : MonoBehaviour
{
    public Transform Target
    {
        get => _target;
    }

    public Action OnTargetEntered; // Event triggered when a target enters the room
    public Action OnTargetExited; // Event triggered when a target exits the room

    private Transform _target; // Reference to the target

    // Called when a Collider enters the trigger zone of the room
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            // Set the player as the current target and invoke the On Target Entered event
            _target = player.transform;
            OnTargetEntered?.Invoke();
        }
        else
        {
            ElementalAI elementalAI = other.GetComponent<ElementalAI>();

            if (elementalAI != null)
            {
                // If an elemental enters, reset the target (set to null)
                _target = null;
            }
            
        }
    }

    // Called when a Collider exits the trigger zone of the room
    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            // Invoke the On Target Exited event when a player exits the room
            OnTargetExited?.Invoke();
        }
    }
}
