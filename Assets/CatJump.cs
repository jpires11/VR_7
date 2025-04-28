using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatJump : MonoBehaviour
{
    public InputActionProperty jumpButton;
    public float jumpHeight = 3f;
    public CharacterController cc;
    public LayerMask groundlayers;

    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    private void Update()
    {
        bool _isGrounded = IsGrounded();

        if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);
    }

    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -0.3f * gravity);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundlayers);
    }
}
