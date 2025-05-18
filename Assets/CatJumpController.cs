using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

[RequireComponent(typeof(InputData))]
public class CatJumpController : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight = 3f;
    public float jumpForwardSpeed = 3f;
    public float minControllersVelocity = 2f;
    public LayerMask groundlayers;

    [Header("Input")]
    public InputActionProperty moveInput;

    [Header("References")]
    public CharacterController cc;
    public ClimbProvider climbProvider; //

    private InputData _inputData;
    private float gravity = -7f;
    private float verticalVelocity = 0f;
    private Vector3 horizontalMovement = Vector3.zero;
    private Transform cameraTransform;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (!enabled || !cc.enabled)
            return;

        if (climbProvider != null && climbProvider.isLocomotionActive)
            return;

        bool isGrounded = IsGrounded();

        // Reset velocity on ground
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
            horizontalMovement = Vector3.zero;
        }

        // Controller-based jump trigger
        if (isGrounded &&
            _inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, 
            out Vector3 leftVelocity) &&
            _inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, 
            out Vector3 rightVelocity))
        {
            if (leftVelocity.magnitude > minControllersVelocity && rightVelocity.magnitude > minControllersVelocity)
            {
                Jump();
            }
        }


        // Gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Final move vector
        Vector3 move = horizontalMovement + Vector3.up * verticalVelocity;
        cc.Move(move * Time.deltaTime);
    }

    private void Jump()
    {
        // Apply vertical velocity
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Read stick input for jump direction
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 inputDir = new Vector3(input.x, 0f, input.y);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            // Convert to camera-relative world direction
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;

            Vector3 worldDir = (camForward.normalized * inputDir.z + camRight.normalized * inputDir.x).normalized;
            horizontalMovement = worldDir * jumpForwardSpeed;
        }
        else
        {
            horizontalMovement = Vector3.zero;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundlayers);
    }
}
