using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
//using UnityEngine.InputSystem;

[RequireComponent(typeof(InputData))]
public class CatJumpControllerBackup : MonoBehaviour
{
    //public InputActionProperty jumpButton;
    public float jumpHeight = 3f;
    public CharacterController cc;
    public LayerMask groundlayers;
    public float minControllersVelocity = 2f;

    private InputData _inputData;
    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    private void Update()
    {
        bool _isGrounded = IsGrounded();

        //if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        //{
        //    Jump();
        //}

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity)
            && _inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity)
            && _isGrounded)
        {
            if (leftVelocity.magnitude > minControllersVelocity && rightVelocity.magnitude > minControllersVelocity)
            {
                Jump();
            }
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
