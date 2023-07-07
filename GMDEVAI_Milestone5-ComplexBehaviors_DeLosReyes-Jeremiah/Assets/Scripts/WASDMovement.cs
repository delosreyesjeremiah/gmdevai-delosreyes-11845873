using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    private float speed = 10.0f;
    private float rotationSpeed = 200.0f;
    private float currentSpeed = 0.0f;

    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }

    void LateUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0.0f, 0.0f, translation);
        currentSpeed = translation;

        transform.Rotate(0.0f, rotation, 0.0f);
    }
}
