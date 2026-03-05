using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform mainCamera;

    void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera);
            // transform.LookAt(transform.position - (mainCamera.position - transform.position));
        }
    }
}
