using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    InputAction MoveCameraAction;
    InputAction RotateCameraLeftAction;
    InputAction RotateCameraRightAction;
    InputAction ZoomInAction;
    InputAction ZoomOutAction;
    private Camera mainCamera;
    private float rotationTime = 1f;
    private float rotationY = 0f;
    private float lastRotationTime = 0f;
    private float rotationSpeed = 135f;

    private void Start()
    {
        mainCamera = Camera.main;
        MoveCameraAction = InputSystem.actions.FindAction("Move");
        RotateCameraLeftAction = InputSystem.actions.FindAction("RotateLeft");
        RotateCameraRightAction = InputSystem.actions.FindAction("RotateRight");
        ZoomInAction = InputSystem.actions.FindAction("ZoomIn");
        ZoomOutAction = InputSystem.actions.FindAction("ZoomOut");
    }

    private void RotateCamera(float degrees)
    {
        lastRotationTime = Time.time;
        rotationY += degrees;
    }

    void Update()
    {
        Vector2 moveValue = MoveCameraAction.ReadValue<Vector2>();
        mainCamera.transform.Translate(new Vector3(moveValue.x, moveValue.y, 0) * Time.deltaTime * 5f, Space.World);

        if ((Time.time - lastRotationTime) > rotationTime)
        {
            if (RotateCameraLeftAction.IsPressed()) RotateCamera(90f);
            if (RotateCameraRightAction.IsPressed()) RotateCamera(-90f);
        }

        // Calculate the rotation for this frame
        float step = rotationSpeed * Time.deltaTime;
        // Smoothly rotate the object from its current rotation to the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rotationY, 0), step);

        // Check if the rotation is complete and reset cooldown
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, rotationY, 0)) < 0.1f)
        {
            lastRotationTime = 0;
        }
    }
}
