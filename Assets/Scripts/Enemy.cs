using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth; 
    public float scoreValue;

    private float currentHealth;
    private HealthBar healthBar;

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHealth = maxHealth;

        if(GetComponentInChildren<HealthBar>())
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(currentHealth < 0)
        {
            Die();
        }

        if(healthBar)
        {
            UpdateHealthBar();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public void Die()
    {
        GameController.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }

    public void UpdateHealthBar()
    {
        healthBar.SetCurrentHealthRatio(GetCurrentHealthRatio());
    }

    public float GetCurrentHealthRatio()
    {
        return currentHealth / maxHealth;
    }
}
