using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar)
        {
            UpdateHealthBar();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void UpdateHealthBar()
    {
        healthBar.SetCurrentHealthRatio(GetCurrentHealthRatio());
    }

    public void RestoreHealth(float healthAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthAmount, 0, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public void DisableHealthBar()
    {
        healthBar.gameObject.SetActive(false);
    }

    public float GetCurrentHealthRatio()
    {
        return currentHealth / maxHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}
