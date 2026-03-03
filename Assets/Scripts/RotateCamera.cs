using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 1f;
    private float rotationPosition = 0f;

    void Update()
    {
        rotationPosition += rotationSpeed;
        transform.rotation = Quaternion.Euler(0, rotationPosition, 0);
    }
}
