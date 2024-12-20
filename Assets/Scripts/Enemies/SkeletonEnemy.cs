using System.Collections;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    private Animator anim;
    public AudioClip stabSound; // Assign this in the Inspector
    private AudioSource audioSource;

    public int damage = 1; // Amount of damage to deal to the player
    private bool isDead = false; // Flag to track if the Skeleton is dead

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        if (isDead) return; // Prevent multiple death triggers
        isDead=true;
        anim.SetTrigger("death"); // Activate the death trigger
        
        // Play the stab sound if assigned
        if (stabSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stabSound);
        }

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        // Wait for the death animation to finish
       

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return; // Skip damaging the player if the Skeleton is dead
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (player != null && playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * 15f, ForceMode2D.Impulse); // Adjust knockback strength
                player.TakeDamage(damage);
            }
        }
    }
}

