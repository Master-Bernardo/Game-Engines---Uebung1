using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekerEnemy : Enemy
{
    public Transform target;
    private NavMeshAgent agent;
    private Rigidbody rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        base.Update();

        if(target)
        {
            agent.SetDestination(target.position);
        }
    }
}
