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
        transform.LookAt(mainCamera);
        // transform.LookAt(transform.position - (mainCamera.position - transform.position));
    }
}
