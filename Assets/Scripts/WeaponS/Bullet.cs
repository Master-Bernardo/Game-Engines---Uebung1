using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    public float startSpeed;
    public float damage = 5;
    public GameObject bulletSpark;

    public bool enemyBullet; //if this is true this is the bullet of the enemy

    void Start()
    {
        rb.velocity = transform.forward * startSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            if(enemyBullet) //falls die Kugel von einem Gegner kommt
            {
                if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Friendly")
                {
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                    Instantiate(bulletSpark, collision.contacts[0].point, transform.rotation);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                    Instantiate(bulletSpark, collision.contacts[0].point, transform.rotation);
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
