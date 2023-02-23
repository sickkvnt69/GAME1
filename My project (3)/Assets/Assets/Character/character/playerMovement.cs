using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool jumping = false;
    bool falling = false;
    Vector2 characterVelocity;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

        }



        characterVelocity = controller.getVelocity();
        UnityEngine.Debug.Log(characterVelocity.y);
       

        if (characterVelocity.y > 0) 
        {
            jumping = true;
            falling = false;
        }else if(characterVelocity.y < 0) 
        {
            falling = true;
            jumping = false;
        }

        UnityEngine.Debug.Log(jumping);

        if (characterVelocity.y < 0 + 3 && characterVelocity.y > 0 - 3 && jumping)
        {
            animator.SetBool("isFloating", true);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        else if (characterVelocity.y < -3 && falling)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isFloating", false);
            animator.SetBool("isJumping", false);
        }
        else if (characterVelocity.y > 0)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isFloating", false);
            animator.SetBool("isJumping", true);
        }
        else if (characterVelocity.y == 0)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isFloating", false);
            animator.SetBool("isJumping", false);
        }
     
    }

    // Move our Character
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime,false,jump);
        jump = false;
    }
}
