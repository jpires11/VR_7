using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerXR : MonoBehaviour
{
    private CharacterController characterController;

    public float moveSpeed = 2f;
    public float jumpForce = 3f;

    public InputActionProperty moveAction;
    public InputActionProperty jumpAction;

    private float gravity = -9.81f;
    private float verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0;

        // Gravity and jump
        if (characterController.isGrounded)
        {
            verticalVelocity = 0;
            if (jumpAction.action.triggered)
                verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = move * moveSpeed + Vector3.up * verticalVelocity;
        characterController.Move(finalMove * Time.deltaTime);
    }
}
