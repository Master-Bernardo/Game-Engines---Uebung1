using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    //public bool friendly = false;
    [Header("Game Logic")]
    [Tooltip("if no enemies nearby, this unit will always follow the target - player is target for companions")]
    public bool followTarget;
    public bool followPlayer; // gemütlicher, dann können wir einfach das ticken und müssen den Player nicht manuel assignen
    public Transform followingTarget;
    [Tooltip("points which we get for killing this guy")]
    public float scoreValue; 
    public float itemDropChance;
    public GameObject[] droppableItems;
    [Tooltip("how distant enemies will he notice?")]
    public float enemyDetectingRadius = 15f;
    public bool alive = true;
    

    [Header("To assign")]
    public GameObject deathParticle;
    public Health health;
    private Renderer rend;
    private Rigidbody rb;
    protected Transform target;
    protected NavMeshAgent agent;

    [Header("Performance Optimisation")]
    [Tooltip("How often do we check for enemies?")]
    public float fightingUpdateIntervall = 0.5f;
    float nextFightingUpdateTime;
   
    protected virtual void Start()
    {
        GameController.Instance.AddUnit(this); //add this unit to the global unit collection
        rend = transform.GetComponentInChildren<Renderer>();
        alive = true;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        nextFightingUpdateTime = Time.time + Random.Range(0,fightingUpdateIntervall);
        //Debug.Log(nextFightingUpdateTime);
    }

    protected virtual void Update()
    {
        if(alive)
        {
            if (health.GetCurrentHealth() <= 0)
            {
                Die();
            }
            else if(Time.time> nextFightingUpdateTime)
            {
                //Debug.Log(nextFightingUpdateTime);
                nextFightingUpdateTime = Time.time + fightingUpdateIntervall;
                FightingUpdate();
            }
        }
        else
        {
            float dissolveFactor = Mathf.Lerp(rend.material.GetFloat("_Dissolve"), 1, 0.01f);
            rend.material.SetFloat("_Dissolve", dissolveFactor);

            if (rend.material.GetFloat("_Dissolve") > 0.85)
            {
                Destroy(gameObject);
            }
        }
    }

    //checks for enemies in area and sets navmesh targets
    protected virtual void FightingUpdate()
    {
        Unit nearestEnemy = GetNearestEnemy();
        //if there are no enemies beside me, go for the player 
        if (nearestEnemy == null)
        {
            if (health.team == Team.Enemy)
            {
                if (GameController.Instance.player)
                {
                    target = GameController.Instance.player.transform;
                }
            }
            else
            {
                target = null;
            }
            
        }
        else if(nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }

        if (target == null && followTarget)
        {
           if (!followPlayer)
           {
                agent.SetDestination(followingTarget.position);
           }
           else
           {
                agent.SetDestination(GameController.Instance.player.transform.position);
           }
            agent.updateRotation = true;
            agent.isStopped = false;
        }
    }

    public void Die()
    {
        if (deathParticle)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
        }
        if (health.team == Team.Enemy) GameController.Instance.AddScore(scoreValue);
        GameController.Instance.RemoveUnit(this);
        SpawnRandomItem();       
        StartFloating();
        health.DisableHealthBar();
        alive = false;
    }

    public void SpawnRandomItem()
    {
        if (Random.value < itemDropChance)
        {
            /*int index = 0;
            if (Random.value < 0.75)
            {
                index = 1;
            }*/
            int index = Random.Range(0, droppableItems.Length);
            Rigidbody newItemRB = Instantiate(droppableItems[index], transform.position, transform.rotation).GetComponent<Rigidbody>();
            newItemRB.AddForce(new Vector3(0, 27500, 0));
            newItemRB.AddTorque(new Vector3(Random.Range(0, 10000), Random.Range(0, 10000), Random.Range(0, 10000)));
        }
    }

    public void StartFloating()
    {
        GetComponentInChildren<NavMeshAgent>().enabled = false;
        rb.useGravity = false;
        rb.isKinematic = false;
        //rb.mass = 0;
        rb.AddForce(new Vector3(0, 100, 0));
    }

    protected Unit GetNearestEnemy()
    {
        HashSet<Unit> enemies = GameController.Instance.GetAllUnits();

        Unit nearestEnemy = null;
        float nearestDistance = float.PositiveInfinity;

        foreach (Unit enemy in enemies)
        {
            if (enemy.health.team != health.team && enemy.health.team != Team.Neutral)
            {
                if (enemy.alive == true && enemy.isActiveAndEnabled)
                {
                    float currentDistance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
                    if (currentDistance < nearestDistance && currentDistance < enemyDetectingRadius)
                    {
                        nearestDistance = currentDistance;
                        nearestEnemy = enemy;
                    }
                }
            }
        }

        if(health.team == Team.Enemy)
        {
            if (GameController.Instance.player)
            {
                float currentDistance = Vector3.Distance(GameController.Instance.player.transform.position, transform.position);
                if (currentDistance < nearestDistance)
                {
                    nearestDistance = currentDistance;
                    nearestEnemy = null; //fall der Player am nähesten ist, setzten wir den nearst Enemy auf null
                }
            }
        }
        return nearestEnemy;
    }

}
