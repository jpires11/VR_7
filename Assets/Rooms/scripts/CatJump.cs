using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class CatJump : MonoBehaviour
{
    public InputActionProperty jumpButton;
    public InputActionProperty moveInput;  // Vector2 stick input
    public float jumpHeight = 3f;
    public float jumpForwardSpeed = 3f; // Controls how much forward momentum is added
    public CharacterController cc;
    public LayerMask groundlayers;

    //private float gravity = Physics.gravity.y;
    private float gravity = -7;
    private Vector3 movement;

    private void Update()
    {
        if (!enabled || !cc.enabled)
            return;

        bool _isGrounded = IsGrounded();

        if (_isGrounded && movement.y < 0)
        {
            movement.y = -2f; // Keeps the character grounded

            // Reset horizontal movement to prevent sliding
            movement.x = 0f;
            movement.z = 0f;
        }

        if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump();
        }

        // Apply gravity
        movement.y += gravity * Time.deltaTime;

        // Move the character
        cc.Move(movement * Time.deltaTime);
    }

    private void Jump()
    {
        // Vertical jump velocity
        movement.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Add forward movement based on joystick direction
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 inputDirection = new Vector3(input.x, 0, input.y);

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // Convert input direction from local to world space
            Vector3 worldDir = transform.TransformDirection(inputDirection.normalized);
            movement.x = worldDir.x * jumpForwardSpeed;
            movement.z = worldDir.z * jumpForwardSpeed;
        }
        else
        {
            // If no stick input, don't apply forward momentum
            movement.x = 0;
            movement.z = 0;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundlayers);
    }
}
