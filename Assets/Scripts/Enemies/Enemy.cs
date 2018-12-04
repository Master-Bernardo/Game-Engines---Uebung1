using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool friendly = false;
    public float scoreValue;
    public Health health;
    public float itemDropChance;
    public GameObject[] droppableItems;
    public GameObject deathParticle;
    public bool alive = true;
    private Renderer rend;
    private Rigidbody rb;
    protected Transform target;
    protected NavMeshAgent agent;

    public float enemyDetectingRadius = 15f;

    //for optimisation
    public float fightingUpdateIntervall = 0.5f;
    float nextFightingUpdateTime;
   
    public virtual void Start()
    {
        GameController.Instance.AddFighter(this);
        rend = transform.GetComponentInChildren<Renderer>();
        alive = true;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        nextFightingUpdateTime = Time.time + fightingUpdateIntervall;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(alive)
        {
            if (health.GetCurrentHealth() <= 0)
            {
                Die();
            }
            else if(Time.time> nextFightingUpdateTime)
            {
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
        Enemy nearestEnemy = GetNearestEnemy();
        //if there are no enemies beside me, go for the player 
        if (nearestEnemy == null)
        {
            if (!friendly)
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
    }

    public void Die()
    {
        if (deathParticle)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
        }
        if (!friendly)GameController.Instance.AddScore(scoreValue);
        GameController.Instance.RemoveFighter(this);
        SpawnRandomItem();       
        StartFloating();
        health.DisableHealthBar();
        alive = false;
    }

    public void SpawnRandomItem()
    {
        if (Random.value < itemDropChance)
        {
            Rigidbody newItemRB = Instantiate(droppableItems[Random.Range(0, droppableItems.Length)], transform.position, transform.rotation).GetComponent<Rigidbody>();
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

    protected Enemy GetNearestEnemy()
    {
        HashSet<Enemy> enemies = GameController.Instance.GetAllFighters();

        Enemy nearestEnemy = null;
        float nearestDistance = float.PositiveInfinity;

        if(friendly)
        {
            foreach(Enemy enemy in enemies)
            {
                if(!enemy.friendly)
                {
                    if(enemy.alive == true)
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
        }else if(!friendly)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.friendly)
                {
                    if (enemy.alive == true)
                    {
                        float currentDistance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
                        if (currentDistance < nearestDistance)
                        {
                            nearestDistance = currentDistance;
                            nearestEnemy = enemy;
                        }
                    }
                }
            }
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
