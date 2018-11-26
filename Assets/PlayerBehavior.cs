using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealthRatio()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damageValue)
    {
        currentHealth -= damageValue;
    }
}
