using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kitty : MonoBehaviour
{
    private Animator anim;
    private bool isRunning = false;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // control movemment
        Vector3 move = Vector3.zero;
        if (Keyboard.current.leftArrowKey.isPressed) move.x -= 0.01f;
        if (Keyboard.current.rightArrowKey.isPressed) move.x += 0.01f;
        if (Keyboard.current.downArrowKey.isPressed) move.z -= 0.01f;
        if (Keyboard.current.upArrowKey.isPressed) move.z += 0.01f;

        if (move != Vector3.zero)
        {
            float speed = isRunning ? runSpeed : walkSpeed;
            transform.Translate(move.normalized * speed * Time.deltaTime);
            anim.SetBool("isMoving", true);
            anim.SetBool("isRunning", isRunning);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // switch to walk mode when c is pressed again
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isRunning = false;
        }

        // switch to run mode when v is pressed
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            isRunning = true;
        }
    }
}