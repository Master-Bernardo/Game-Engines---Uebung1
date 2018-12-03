using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekerEnemy : Enemy
{
    public Transform target;
    private NavMeshAgent agent;

    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        base.Update();

        if(target && alive)
        {
            agent.SetDestination(target.position);
        }
    }
}
