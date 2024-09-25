using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private PrincessHeart heart;

    private UIManager uiManager;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;

    // Awake is called when you load the script
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        uiManager = FindAnyObjectByType<UIManager>();


    }

    // Called once per frame
    private void Update()
    {
        HandleInput();
        UpdateMovement();
        UpdateAnimation();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void UpdateMovement()
    {
        // Check if player is moving right or left
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;  // Face right
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);  // Face left

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            // Horizontal movement
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (IsOnWall() && !IsGrounded())
            {
                rb.gravityScale = 0;              // Stop falling
                rb.velocity = Vector2.zero;       // Stop all movement
            }
            else
            {
                rb.gravityScale = 7;              // Reset gravity
            }
            // Jump
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) //GetKeyDown only gets called on one frame when the user starts press it
                    SoundManager.Instance.PlaySound(jumpSound);
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;  // Increase cooldown over time/ deltaTime: the amount of time that has passed since the last frame update.
        }
    }

    private void UpdateAnimation()
    {
        // Set Animator parameters for running and grounded states
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", IsGrounded());
    }

    // Handles the jump logic
    private void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jump");  // Trigger jump animation
        }
        else if (IsOnWall() && !IsGrounded())
        {
            if (horizontalInput == 0)  // If no horizontal input, jump away from the wall
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);  // Push horizontally
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);  // Flip player direction
            }
            else  // If moving towards a wall, do a wall jump
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);  // Jump away from the wall
                wallJumpCooldown = 0;  // Reset wall jump cooldown
            }
        }
    }

    // Checks if the player is grounded
    private bool IsGrounded()
    {
        // BoxCast downward from the center of the player's collider
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;  // Return true if it hits the ground
    }
    // Checks if the player is touching a wall
    private bool IsOnWall()
    {
        // BoxCast sideways from the center of the player's collider
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return rayCastHit.collider != null;  // Return true if it hits a wall
    }


    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !IsOnWall();
    }

    private void Win()
    {
        animator.SetTrigger("win");
        uiManager.Win();
    }
    private IEnumerator HandleWin()
    {

        heart.TriggerHeartAnimation();
        yield return new WaitForSeconds(1);
        Win();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Princess"))
        {
            StartCoroutine(HandleWin());

        }
    }

}