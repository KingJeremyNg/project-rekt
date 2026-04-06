using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        // 1. Define the plane (e.g., at the origin, facing Up)
        Plane plane = new Plane(mainCamera.forward, mainCamera.position);

        // 2. Find the closest point on that plane to this object
        Vector3 targetPoint = plane.ClosestPointOnPlane(transform.position);

        // 3. Make the object look at that point
        transform.LookAt(targetPoint);
    }
}
