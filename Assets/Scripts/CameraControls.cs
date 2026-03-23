using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    InputAction MoveCameraAction;
    // InputAction RotateCameraLeftAction;
    // InputAction RotateCameraRightAction;
    // private float rotationTime = 5f;
    // private float rotationY = 0f;
    // private float lastRotationTime = 0f;
    // private float rotationSpeed = 5f;

    private void Start()
    {
        MoveCameraAction = InputSystem.actions.FindAction("Move");
        // RotateCameraLeftAction = InputSystem.actions.FindAction("RotateLeft");
        // RotateCameraRightAction = InputSystem.actions.FindAction("RotateRight");
    }

    // private void RotateCamera(float degrees)
    // {
    //     rotationY += degrees;
    //     print(rotationY);
    //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotationY, 0), Time.deltaTime * rotationSpeed);
    // }

    void Update()
    {
        Vector2 moveValue = MoveCameraAction.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveValue.x, moveValue.y, 0) * Time.deltaTime * 5f, Space.World);

        // if (Time.time - lastRotationTime > rotationTime)
        // {
        //     if (RotateCameraLeftAction.IsPressed())
        //     {
        //         RotateCamera(90f);
        //     }
        //     if (RotateCameraRightAction.IsPressed())
        //     {
        //         RotateCamera(-90f);
        //     }
        // }
    }
}
