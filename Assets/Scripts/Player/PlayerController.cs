using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck), typeof(Jump), typeof(Shoot))]
public class PlayerController : MonoBehaviour
{
    private bool isPaused;
    private int _lives;

    public float dashSpeed = 15f; // Speed during dash
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 1f; // Cooldown between dashes

    private bool isDashing = false;
    private float dashCooldownTimer = 0f; //timer to track cooldown
    public float dashForce = 24f;
    public float direction;




    public int lives

    {
        get => _lives;
        set
        {
            //do valid checking
            if (value > 0)
            { //gameover should be called here
            }

            if (_lives > value)
            {
                //respawn
            }

            _lives = value;
            Debug.Log($"{_lives} lives left");
        }
    }

    private int _score;
    public int score
    {
        get => _score;
        set
        {
            //this can't happen - the score can't be lower than zero so stop this from setting the score
            if (value > 0) return;

            _score = value;
            Debug.Log($"Current Score: {_score}");
        }
    }

    //Component References
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    GroundCheck gc;

    //Movement variables
    [Range(3, 10)]
    public float speed = 5.5f;
    [Range(3, 10)]
    public float jumpForce = 3f;

    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gc = GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // Skip input processing if the game is paused
        if (PauseManager.IsGamePaused())
        {
            return;
        }

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        CheckIsGrounded();
        float hInput = Input.GetAxis("Horizontal");

        if (curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Fire"))
            {
                rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            }

        }
        // Skip input processing if the game is paused

        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);

        //inputs for firing and jump attack
        if (Input.GetButtonDown("Fire1") && isGrounded) anim.SetTrigger("fire");
        if (Input.GetButtonDown("Fire1") && !isGrounded) anim.SetTrigger("jumpAttack");
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("fire");


        }


        //alternate way to sprite flip
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
    }
    private void HandleMovement()
    {
        // Skip regular movement during dash
        if (isDashing) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Flip player sprite based on direction
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
    }

    private void HandleDash()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        // Trigger dash animation and fire attack
        anim.SetTrigger("fire");

        // Apply a force in the direction the player is facing
        float direction = transform.localScale.x; // 1 for right, -1 for left
        rb.AddForce(new Vector2(direction * dashForce, 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        // End dash
        isDashing = false;
    }
    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.velocity.y <= 0) isGrounded = gc.IsGrounded();
        }
        else isGrounded = gc.IsGrounded();
    }

    public void JumpPowerup()
    {
        StartCoroutine(GetComponent<Jump>().JumpHeightChange());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickup curPickup = collision.GetComponent<IPickup>();
        if (curPickup != null)
        {
            curPickup.Pickup(gameObject);
        }
    }
}