using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : Enemy
{
    public float attackDamage;
    public float explosionRadius;
    public float explosionForce;

    public GameObject explosionParticle;

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
        }
    }

    protected override void FightingUpdate()
    {
        base.FightingUpdate();
        if (target)
        {
            agent.SetDestination(target.position);
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

            if(hit.collider.GetComponent<Health>()!= null)
            {
                hit.collider.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

        Instantiate(explosionParticle, transform.position, transform.rotation);
        Die();
    }
}
