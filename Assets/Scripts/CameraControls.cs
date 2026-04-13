using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public static CameraControls Instance { get; private set; }
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
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        initialPosition = transform.GetChild(0).position;
        initialRotation = transform.GetChild(0).rotation;
        mainCamera = Camera.main;
        MoveCameraAction = InputSystem.actions.FindAction("Move");
        RotateCameraLeftAction = InputSystem.actions.FindAction("RotateLeft");
        RotateCameraRightAction = InputSystem.actions.FindAction("RotateRight");
        ZoomInAction = InputSystem.actions.FindAction("ZoomIn");
        ZoomOutAction = InputSystem.actions.FindAction("ZoomOut");
    }

    public void RotateCamera(float degrees)
    {
        lastRotationTime = Time.time;
        rotationY += degrees;
    }

    public void ResetCamera()
    {
        mainCamera.orthographicSize = 5f;
        mainCamera.transform.rotation = initialRotation;
        mainCamera.transform.position = initialPosition;
    }

    private void NarrativeCamera()
    {
        float SineWave = Mathf.Sin(Time.time * 0.5f) * 0.1f;
        Vector3 dolly = new Vector3(SineWave, Mathf.Abs(SineWave), 0);
        Vector3 targetPosition = GameManager.Instance.NarrativeCameraTarget.position;
        Vector3 offset = new Vector3(0, 0.25f, -1.5f);
        Vector3 cameraPosition = targetPosition + offset + dolly;
        mainCamera.orthographicSize = 1f;
        mainCamera.transform.position = cameraPosition;
        mainCamera.transform.LookAt(targetPosition);
    }

    private void GameCamera()
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

    void Update()
    {
        if (GameManager.Instance.NarrativeCameraTarget != null)
        {
            NarrativeCamera();
        }
        else
        {
            GameCamera();
        }
    }
}
