using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

[RequireComponent(typeof(InputData))]
public class CatJumpControllerVignette : MonoBehaviour, ITunnelingVignetteProvider
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
    public ClimbProvider climbProvider;
    public TunnelingVignetteController vignetteController;

    [Header("Vignette Settings")]
    [Range(0f, 1f)]
    public float vignetteApertureSize = 0.3f;
    [Range(0f, 1f)]
    public float vignetteFeathering = 0.2f;

    private InputData _inputData;
    private float gravity = -7f;
    private float verticalVelocity = 0f;
    private Vector3 horizontalMovement = Vector3.zero;
    private Transform cameraTransform;
    private bool vignetteActive = false;

    public VignetteParameters vignetteParameters => new VignetteParameters
    {
        apertureSize = vignetteApertureSize,
        featheringEffect = vignetteFeathering,
        easeInTime = 0.2f,
        easeOutTime = 0.3f,
        easeInTimeLock = false,
        easeOutDelayTime = 0.1f,
        vignetteColor = Color.black,
        vignetteColorBlend = Color.black,
        apertureVerticalPosition = 0f
    };

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

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
            horizontalMovement = Vector3.zero;

            if (vignetteActive)
            {
                vignetteController.EndTunnelingVignette(this);
                vignetteActive = false;
            }
        }

        if (isGrounded &&
            _inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity) &&
            _inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {
            if (leftVelocity.magnitude > minControllersVelocity && rightVelocity.magnitude > minControllersVelocity)
            {
                Jump();
            }
        }

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 move = horizontalMovement + Vector3.up * verticalVelocity;
        cc.Move(move * Time.deltaTime);
    }

    private void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 inputDir = new Vector3(input.x, 0f, input.y);

        if (inputDir.sqrMagnitude > 0.01f)
        {
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

        if (!vignetteActive)
        {
            vignetteController.BeginTunnelingVignette(this);
            vignetteActive = true;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundlayers);
    }
}
