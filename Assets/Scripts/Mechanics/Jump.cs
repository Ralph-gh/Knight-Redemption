using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;

    [SerializeField, Range(2, 15)] private float jumpHeight = 5;
    [SerializeField, Range(1, 20)] private float jumpFallForce = 50;

    float timeHeld;
    float maxHoldTime = 0.5f;
    float jumpInputTime = 0;
    float calculatedJumpForce;

    public bool jumpCancelled = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();

        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isGrounded) jumpCancelled = false;

        if (Input.GetButtonDown("Jump")) jumpInputTime = Time.time;
        if (Input.GetButton("Jump")) timeHeld += Time.deltaTime;
        if (Input.GetButtonUp("Jump"))
        {
            timeHeld = 0;
            jumpInputTime = 0;
            
            if (rb.velocity.y < -10) return;
            jumpCancelled = true;
        }

        if (jumpInputTime != 0 && (jumpInputTime + timeHeld) < (jumpInputTime + maxHoldTime))
        {
            if (pc.isGrounded)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
            }
        }

        if (jumpCancelled) rb.AddForce(Vector2.down * jumpFallForce);
    }

    public IEnumerator JumpHeightChange()
    {
        jumpHeight = jumpHeight * 2;
        Debug.Log($"Jump Height Buffed: {jumpHeight}");
        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));

        yield return new WaitForSeconds(5);

        jumpHeight = jumpHeight / 2;
        Debug.Log($"Jump Height Nerfed: {jumpHeight}");
        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }
}
