using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is fine for a smaller scope but it becomes harder to scale
public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Oneup,
        Coin,
        Flower,
        Mushroom,
        Star,
        Telekenisis,
    }

    public PickupType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with a collectible: " + type);
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Oneup:
                    pc.lives++;
                    break;
                case PickupType.Coin:
                    pc.score++;
                    break;
                case PickupType.Flower:
                    pc.score++;
                    break;
                case PickupType.Mushroom:
                    pc.score++;
                    break;
                case PickupType.Star:
                    pc.score++;
                    break;
                case PickupType.Telekenisis:
                    pc.score++;
                    break;
            }

            Destroy(gameObject);
        }
    }
}
