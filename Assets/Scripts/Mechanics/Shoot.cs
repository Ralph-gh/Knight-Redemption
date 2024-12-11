using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    public Vector2 initalShotVelocity = Vector2.zero;

    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initalShotVelocity == Vector2.zero)
        {
            Debug.Log("Initial shot velocity is zero, changing it to a default value");
            initalShotVelocity.x = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log($"Please set default values on the shoot script for {gameObject.name}");

        //initalShotVelocityLeft = initalShotVelocity;
        //initalShotVelocityLeft.x *= -1;
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            
            curProjectile.SetVelocity(new Vector2(initalShotVelocity.x, initalShotVelocity.y));
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(new Vector2(-initalShotVelocity.x, initalShotVelocity.y));
        }
    }
}
