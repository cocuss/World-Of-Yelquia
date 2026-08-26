using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedVelocity = -2f;

    [Header("References")]
    [SerializeField] private Animator animator;


    private CharacterController characterController;
    private InputAction moveAction;

    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Player/Move");

        if (moveAction == null)
        {
            Debug.LogError("No se encontró la acción Player/Move.");
        }
    }

    private void OnEnable()
    {
        moveAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 movement =
            transform.right * input.x +
            transform.forward * input.y;

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        Vector3 finalMovement = movement * moveSpeed;

        finalMovement.y = verticalVelocity;

        characterController.Move(finalMovement * Time.deltaTime);

        float speed = movement.magnitude;

        animator.SetFloat("Speed", speed);
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = groundedVelocity;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }
}