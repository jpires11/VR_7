using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;

public class CatJumpVignette : MonoBehaviour, ITunnelingVignetteProvider
{
    [Header("Jump Settings")]
    public InputActionProperty jumpButton;
    public InputActionProperty moveInput;
    public float jumpHeight = 3f;
    public float jumpForwardSpeed = 3f;
    public CharacterController cc;
    public LayerMask groundlayers;

    [Header("Comfort Vignette")]
    public TunnelingVignetteController vignetteController;
    [Range(0f, 1f)]
    public float vignetteApertureSize = 0.3f;
    [Range(0f, 1f)]
    public float vignetteFeathering = 0.2f;

    private float gravity = -7f;
    private Vector3 movement;
    private bool vignetteActive = false;

    // Dynamically return vignette parameters with user-defined values
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

    private void Update()
    {
        if (!enabled || !cc.enabled)
            return;

        bool isGrounded = IsGrounded();

        if (isGrounded && movement.y < 0)
        {
            movement.y = -2f;
            movement.x = 0f;
            movement.z = 0f;

            if (vignetteActive)
            {
                vignetteController.EndTunnelingVignette(this);
                vignetteActive = false;
            }
        }

        if (jumpButton.action.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime;
        cc.Move(movement * Time.deltaTime);
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 inputDirection = new Vector3(input.x, 0, input.y);

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Vector3 worldDir = transform.TransformDirection(inputDirection.normalized);
            movement.x = worldDir.x * jumpForwardSpeed;
            movement.z = worldDir.z * jumpForwardSpeed;
        }
        else
        {
            movement.x = 0;
            movement.z = 0;
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
