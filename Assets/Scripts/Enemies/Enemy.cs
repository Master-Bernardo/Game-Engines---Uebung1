using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float scoreValue;
    public Health health;
    public float itemDropChance;
    public GameObject[] droppableItems;
    public bool alive = true;
    private Renderer rend;
    private Rigidbody rb;
   
    public virtual void Start()
    {
        rend = transform.GetComponentInChildren<Renderer>();
        alive = true;
        rb = GetComponent<Rigidbody>();
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

    public void Die()
    {
        GameController.Instance.AddScore(scoreValue);
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

}
