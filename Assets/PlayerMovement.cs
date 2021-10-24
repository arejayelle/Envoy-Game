using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    private float horizontalMove = 0f;
    public float runSpeed = 40f;

    private bool jump = false;
    private bool crouch = false;
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = !crouch;
            animator.SetBool("isCrouching", crouch);
        }
        
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.deltaTime,crouch, jump);
        jump = false;
    }
}
