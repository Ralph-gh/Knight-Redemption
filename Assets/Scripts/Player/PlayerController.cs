using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    private bool isPaused;
    private int _lives;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip dashSound;

    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                // Game over logic
                Debug.Log("Game Over");
            }

            if (_lives > value)
            {
                // Respawn logic
                Debug.Log("Player lost a life.");
            }

            _lives = value;
            Debug.Log($"Lives left: {_lives}");
        }
    }

    private int _score;
    public int score
    {
        get => _score;
        set
        {
            if (value < 0) return; // Prevent negative scores

            _score = value;
            Debug.Log($"Current Score: {_score}");
        }
    }

    // Component References
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck gc;

    // Movement variables
    [Range(3, 10)]
    public float speed = 5.5f;
    [Range(3, 10)]
    public float jumpForce = 3f;

    public bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PauseManager.IsGamePaused()) return; // Skip updates if the game is paused

        CheckIsGrounded();
        HandleMovement();
        HandleDash();
    }

    private void HandleMovement()
    {
        if (isDashing) return; // Skip movement during dashing

        float hInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (hInput != 0)
        {
            sr.flipX = hInput < 0;
        }

        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            anim.SetTrigger("fire");
            audioSource.PlayOneShot(attackSound);

        }
        else if (Input.GetButtonDown("Fire1") && !isGrounded)
        {
            anim.SetTrigger("jumpAttack");
            audioSource.PlayOneShot(attackSound);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            anim.SetTrigger("jump");
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        // Determine dash direction based on sprite orientation
        float dashDirection = sr.flipX ? -1f : 1f;
        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        audioSource.PlayOneShot(dashSound);

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        rb.gravityScale = originalGravity;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void CheckIsGrounded()
    {
        if (isDashing) return;
        isGrounded = gc.IsGrounded();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickup curPickup = collision.GetComponent<IPickup>();
        if (curPickup != null)
        {
            curPickup.Pickup(gameObject);
        }
    }

    public void JumpPowerup()
    {
        StartCoroutine(GetComponent<Jump>().JumpHeightChange());
    }
}
