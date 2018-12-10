using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    public float lifeTime;
    public float startSpeed;
    public float damage = 5;
    public GameObject bulletSpark;

    //public bool enemyBullet; //if this is true this is the bullet of the enemy
    public Team team; //which team shot this bullet? so the teams dont hit themselves

    protected virtual void Start()
    {
        rb.velocity = transform.forward * startSpeed;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            Health colliderHealth = collision.gameObject.GetComponent<Health>();
            if (colliderHealth.team != team) //falls die Kugel von einem Gegner kommt
            {
                colliderHealth.TakeDamage(damage);
                Instantiate(bulletSpark, collision.contacts[0].point, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
