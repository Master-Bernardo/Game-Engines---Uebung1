﻿using System.Collections;
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
        //set target
        bool searchForNewTarget = false;
        if (!target)
        {
            searchForNewTarget = true;
        }
        else if (target.GetComponent<Enemy>() != null)
        {
            if (target.GetComponent<Enemy>().alive == false) searchForNewTarget = true;
        }

        if (searchForNewTarget)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            //if there are no enemies beside me, go for the player 
            if (nearestEnemy == null && !friendly)
            {
                if (GameController.Instance.player)
                {
                    target = GameController.Instance.player.transform;
                }
            }
            else if(nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
        }
    }

    public void Die()
    {
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
            newItemRB.AddForce(new Vector3(0, 100, 0));
            newItemRB.AddTorque(new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)));
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

    Enemy GetNearestEnemy()
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
                    float currentDistance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
                    if (currentDistance < nearestDistance)
                    {
                        nearestDistance = currentDistance;
                        nearestEnemy = enemy;
                    }  
                }
            }
        }else if(!friendly)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.friendly)
                {
                    float currentDistance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
                    if (currentDistance < nearestDistance)
                    {
                        nearestDistance = currentDistance;
                        nearestEnemy = enemy;
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
