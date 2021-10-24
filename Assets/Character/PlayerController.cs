using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Variables set in the inspector
    [SerializeField] private float mRunSpeed;
    [SerializeField] private float mJumpForce;
    [SerializeField] private LayerMask mWhatIsGround;
    float horizontalMove = 0f;


    private float kGroundCheckRadius = 0.1f;

    // Booleans used to coordinate with the animator's state machine
    private bool isMoving;
    private bool isGrounded;
    private bool isFalling;
    private bool isJumping;

    // References to Player's components
    public Rigidbody2D rb;
    public Animator Animator;
    private Transform mGroundCheck;

    private bool mIsFacingRight = true;
    
    private void Start()
    {
        mGroundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*mRunSpeed;

        isMoving = Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.01f ;
        Animator.SetBool("isMoving", isMoving);
        
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            // Animator.SetBool("isJumping", isJumping);
            // Animator.SetTrigger("jump");

        }
        // if (Input.GetButtonDown("Crouch"))
        // {
        //     crouch = !crouch;
        //     Animator.SetBool("isCrouching", crouch);
        // }

        Animator.SetBool("isGrounded", isGrounded);
        Animator.SetBool("isFalling", isFalling);

    }


    private void FixedUpdate()
    {
        MoveCharacter(horizontalMove*Time.deltaTime, isJumping);
        UpdateFalling();
        UpdateGrounded();
        isJumping = false;

    }

    // Lab 3
    private void UpdateGrounded()
    {
        var wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        foreach(Collider2D col in colliders)
        {
            if(col.gameObject != gameObject)
            {
                isGrounded = true;
                if(!wasGrounded) OnLand();
                
            }
        }
    }

    private void OnLand()
    {
        isFalling = false;
        isJumping = false;
        Animator.SetBool("isFalling", false);
        Animator.SetBool("isGrounded", true);
    }
    // Lab 3
    
    private float mMovementSmoothing = 0.05f;
    private Vector3 mVelocity = Vector3.zero;
    
    public void MoveCharacter(float move, bool jump)
    {
        if (true)
        {
            var targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            // smooth movement and apply
            rb.velocity =
                Vector3.SmoothDamp(rb.velocity, targetVelocity, ref mVelocity, mMovementSmoothing);
            
            // Input is moving right and the player is facing left...
            if (move > 0 && !mIsFacingRight)
            {
                Flip();
            }
            else if (move < 0 && mIsFacingRight)
            {
                Flip();
            }
            if (isGrounded && jump)
            {
                isGrounded = false;
                isJumping = true;
                Animator.SetTrigger("jump");
                rb.AddForce(new Vector2(0f, mJumpForce), ForceMode2D.Impulse);
            }
        }
    }

    private void UpdateFalling()
    {
        isFalling = rb.velocity.y < 0.0f;
        if (isFalling && !isJumping)
            OnFall();
    }

    private void OnFall()
    {
        Animator.SetBool("isFalling", true);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        mIsFacingRight = !mIsFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
