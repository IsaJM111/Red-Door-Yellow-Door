using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -20f;

    [Header("Crouching")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private float crouchingSpeed = 2.5f;

    [Header("Sprinting")]
    [SerializeField] private float sprintingSpeed = 10f;

    private CharacterController controller;
    private float verticalVelocity;
    private bool isCrouching;
    private bool isSprinting;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Make sure the controller starts at standing height.
        controller.height = standingHeight;
    }
    private void Update()
    {
        HandleCrouch();
        HandleSprint();
        HandleMovement();
    }
    private void HandleCrouch()
    {
        // Press C to toggle crouching.
        if (Keyboard.current != null &&
            Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCrouching = !isCrouching;
        }

        if (isCrouching)
        {
            controller.height = crouchingHeight;
        }
        else
        {
            controller.height = standingHeight;
        }
    }
    private void HandleSprint()
    {
        // Hold Left Shift to sprint.
        if (Keyboard.current != null)
        {
            isSprinting = Keyboard.current.leftShiftKey.isPressed;
        }
    }
    private void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            input.x = Keyboard.current.dKey.isPressed ? 1f : 0f;
            input.x -= Keyboard.current.aKey.isPressed ? 1f : 0f;

            input.y = Keyboard.current.wKey.isPressed ? 1f : 0f;
            input.y -= Keyboard.current.sKey.isPressed ? 1f : 0f;
        }
        Vector3 move =
            transform.right * input.x +
            transform.forward * input.y;
        if (move.sqrMagnitude > 1f)
        {
            move.Normalize();
        }
        // Use slower movement while crouching.
        float currentSpeed = isCrouching
            ? crouchingSpeed
            : isSprinting
                ? sprintingSpeed
                : speed;
        // Gravity.
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        move.y = verticalVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
}