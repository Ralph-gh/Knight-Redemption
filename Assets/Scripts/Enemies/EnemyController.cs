using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of movement
    public Transform leftBoundary; // Position for left boundary
    public Transform rightBoundary; // Position for right boundary
    private bool movingRight = true; // Direction flag

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            // Check if we've reached the right boundary
            if (transform.position.x >= rightBoundary.position.x)
            {
                FlipDirection();
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            // Check if we've reached the left boundary
            if (transform.position.x <= leftBoundary.position.x)
            {
                FlipDirection();
            }
        }
    }

    private void FlipDirection()
    {
        movingRight = !movingRight;
        sr.flipX = !sr.flipX; // Flip sprite horizontally
    }

    private void OnDrawGizmos()
    {
        // Visualize boundaries in the Scene view
        Gizmos.color = Color.red;
        if (leftBoundary != null) Gizmos.DrawLine(transform.position, leftBoundary.position);
        if (rightBoundary != null) Gizmos.DrawLine(transform.position, rightBoundary.position);
    }
}
