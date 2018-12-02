using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : SeekerEnemy
{
    public float attackDamage;
    public float explosionRadius;
    public float explosionForce;

    public GameObject explosionParticle;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (alive)
        {
            if (target)
            {
                if (Vector3.Distance(transform.position, target.position) < explosionRadius)
                {
                    Explode();
                }
            }
            else
            {
                if (GameController.Instance.player)
                {
                    target = GameController.Instance.player.transform;
                }
            }
        }
        else
        {

        }

    }

    public void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRadius, transform.forward);

        foreach(RaycastHit hit in hits)
        {
            Vector3 direction = target.position - transform.position;

            if(hit.collider.GetComponent<Rigidbody>())
            {
                hit.collider.GetComponent<Rigidbody>().AddForce(direction * explosionForce, ForceMode.Impulse);
            }

            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

        Instantiate(explosionParticle, transform.position, transform.rotation);
        Die();
    }
}
