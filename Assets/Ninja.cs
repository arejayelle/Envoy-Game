using UnityEngine;

public class Ninja : MonoBehaviour
{
    // Variables set in the inspector
    [SerializeField] private float mRunSpeed;
    [SerializeField] private float mJumpForce;
    [SerializeField] private LayerMask mWhatIsGround;

    private float kGroundCheckRadius = 0.1f;

    // Booleans used to coordinate with the animator's state machine
    private bool mMoving;
    private bool mGrounded;
    private bool mFalling;

    // References to Player's components
    private Animator mAnimator;
    private Rigidbody2D mRigidBody2D;
    private SpriteRenderer mSpriteRenderer;
    private Transform mGroundCheck;

    private void Start()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mSpriteRenderer = transform.Find("NinSprite").GetComponent<SpriteRenderer>();
        mGroundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    // lab 3
    private void Update()
    {
        ResetTriggers();
        
        UpdateGrounded();
        MoveCharacter();
        UpdateFalling();
        UpdateAttack();
        // TODO: Update animator's variables
        mAnimator.SetBool("isRunning", mMoving);
        mAnimator.SetBool("isFalling", mFalling);
        mAnimator.SetBool("isGrounded", mGrounded);

    }

    private void ResetTriggers()
    {
        mAnimator.ResetTrigger("WipeAttack");
        mAnimator.ResetTrigger("Jump");
    }

    // Lab 3
    private void UpdateGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        foreach(Collider2D col in colliders)
        {
            if(col.gameObject != gameObject)
            {
                mGrounded = true;
                return;
            }
        }
        mGrounded = false;
    }
    // Lab 3
    private void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        mMoving = !Mathf.Approximately(horizontal, 0f);
        if (mMoving)
        {
            transform.Translate(horizontal * mRunSpeed * Time.deltaTime, 0, 0);
            FaceDirection(horizontal < 0f ? Vector2.left : Vector2.right);
        }

        if (Input.GetButtonDown("Jump"))
        {
            mAnimator.SetTrigger("Jump");
            mRigidBody2D.AddForce(new Vector2(0, mJumpForce), ForceMode2D.Impulse);
        }
    }

    private void UpdateFalling()
    {
        mFalling = mRigidBody2D.velocity.y < 0.0f;
    }

    private void FaceDirection(Vector2 direction)
    {
        // Flip the sprite
        mSpriteRenderer.flipX = direction == Vector2.right ? false : true;
    }

    private void UpdateAttack()
    {
        var isAttacking = Input.GetButton("Fire1");
        if (isAttacking)
        {
            mAnimator.SetTrigger("WipeAttack");
        }
    }
}
