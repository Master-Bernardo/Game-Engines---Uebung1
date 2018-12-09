using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb;
    public float startSpeed;
    public GameObject explosionParticle;
    public float explosionRadius;
    public float explosionForce;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Player")
        {
            Explode();
        }
        
    }

    public void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRadius, transform.forward);

        foreach (RaycastHit hit in hits)
        {
            Vector3 direction = hit.collider.transform.position - transform.position;

            if (hit.collider.GetComponent<Rigidbody>())
            {
                hit.collider.GetComponent<Rigidbody>().AddForce(direction * explosionForce, ForceMode.Impulse);
            }

            if (hit.collider.GetComponent<Health>())
            {
                hit.collider.GetComponent<Health>().TakeDamage(damage);
            }
        }

        Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
