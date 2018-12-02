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

    void Start()
    {
        rb.velocity = transform.forward * startSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Instantiate(bulletSpark, collision.contacts[0].point, transform.rotation);
            Destroy(gameObject);
        }
    }
}
