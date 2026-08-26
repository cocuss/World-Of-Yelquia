using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera")]
    [SerializeField] private CinemachineCamera playerCamera;

    [Header("Rotation")]
    [SerializeField] private float rotationSensitivity = 0.15f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 8f;

    private InputAction lookAction;

    private float yaw;
    private float pitch;

    private void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Player/Look");

        if (lookAction == null)
        {
            Debug.LogError("No se encontró la acción Player/Look.");
        }

        if (target != null)
        {
            yaw = target.eulerAngles.y;
        }
    }

    private void OnEnable()
    {
        lookAction?.Enable();
    }

    private void OnDisable()
    {
        lookAction?.Disable();
    }

    private void LateUpdate()
    {
        if (target == null || playerCamera == null)
            return;

        FollowTarget();
        HandleRotation();
        HandleZoom();
    }

    private void FollowTarget()
    {
        transform.position = target.position + Vector3.up * 1.5f;
    }

    private void HandleRotation()
    {
        // La cámara solamente gira mientras mantenemos RMB.
        if (!Mouse.current.rightButton.isPressed)
            return;

        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        yaw += lookInput.x * rotationSensitivity;
        pitch -= lookInput.y * rotationSensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // El CameraPivot rota para controlar la cámara.
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // El personaje gira horizontalmente con la cámara.
        target.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    private void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) < 0.01f)
            return;

        CinemachineThirdPersonFollow thirdPersonFollow =
            playerCamera.GetComponent<CinemachineThirdPersonFollow>();

        if (thirdPersonFollow == null)
            return;

        float newDistance =
            thirdPersonFollow.CameraDistance - scroll * zoomSpeed;

        thirdPersonFollow.CameraDistance = Mathf.Clamp(
            newDistance,
            minZoom,
            maxZoom
        );
    }
}