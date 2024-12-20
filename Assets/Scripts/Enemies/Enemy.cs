using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
   
    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] protected int maxHealth;


    public virtual void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();

        if (maxHealth <= 0) maxHealth = 7;
        health = maxHealth;


    }

    public virtual void TakeDamage(int damageValue)

    {
        health -= damageValue;
        if (health <= 0) {
            anim.SetTrigger("Death");

            Destroy(gameObject, 2);

        }

    }
}