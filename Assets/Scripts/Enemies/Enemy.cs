using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float scoreValue;
    public Health health;

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
        Destroy(gameObject);
    }

}
