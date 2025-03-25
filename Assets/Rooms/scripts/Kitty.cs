using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kitty : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 使用新的Input System检测按键
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            anim.SetTrigger("run");
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            anim.SetTrigger("walk");
        }
    }
}