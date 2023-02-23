using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    private float moveHorizontal;
    private float moveVertical;
    private bool faceRight = true;

    public Animator animator;

    public float jump;

    public bool isJumping;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal != 0) 
        {
            rb.velocity = new Vector2(speed * moveHorizontal, rb.velocity.y);
        }

        if (moveHorizontal > 0 && !faceRight)
        {
            Flip();
        }

        if (moveHorizontal < 0 && faceRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        

        // Check if player is floating inbetween jumping and falling
        if (rb.velocity.x == 0 && isJumping)
        {
            // For setting the isJumping parameter in animations
            animator.SetBool("isFloating", true);
        }
        else 
        {
            animator.SetBool("isFloating", false);
        }

        

        // For setting the speed parameter in animations
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            // For setting the isJumping parameter in animations
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;

            // For setting the isJumping parameter in animations
            animator.SetBool("isJumping", true);
        }
    }


    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }

}
