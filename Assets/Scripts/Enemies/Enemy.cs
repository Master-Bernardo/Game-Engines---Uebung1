using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float scoreValue;
    public Health health;
    public float itemDropChance;
    public GameObject[] droppableItems;

    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(health.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameController.Instance.AddScore(scoreValue);
        SpawnRandomItem();
        Destroy(gameObject);
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

}
