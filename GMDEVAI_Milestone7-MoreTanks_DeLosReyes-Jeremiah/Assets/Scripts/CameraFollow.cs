using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // Reference to the target's transform
    [SerializeField] private Vector3 _offset; // Offset between the target and camera

    private void Update()
    {
        if (_target != null)
        {
            // Update the camera's position to match the player's position with the offset
            transform.position = _target.position + _offset;
        }
    }
}
