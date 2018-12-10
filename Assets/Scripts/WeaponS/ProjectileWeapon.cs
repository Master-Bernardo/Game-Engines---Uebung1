using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    
    [SerializeField]
    protected Transform shootPoint; //point from which the projectiles are being shot
    public bool automaticTrigger;
    public float fireRate;
    public float reloadTime;
    public int magazineSize;
    public float bloom;
    protected int currentMagazineAmmo;

    private float fireRateTimer;

    [SerializeField]
    protected GameObject projectile;

    public AmmoType ammoType;

    private void Start()
    {
        Reset();
        fireRateTimer = fireRate;
    }

    private void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if (isEquipped)
        {
            if (automaticTrigger)
            {
                if (Input.GetMouseButton(0) && fireRateTimer <= 0)
                {
                    Shoot();
                    fireRateTimer = fireRate;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && fireRateTimer <= 0)
                {
                    Shoot();
                    fireRateTimer = fireRate;
                }
            }
        }
    }


    public virtual void Shoot()
    {
        Debug.Log("piu piu");
    }

    public virtual void Reload()
    {
        int totalAmmo = WeaponSystem.Instance.GetAmmo(ammoType);
        int ammoDelta = magazineSize - currentMagazineAmmo;
        if (totalAmmo > 0 && totalAmmo >= ammoDelta)
        {
            totalAmmo -= ammoDelta;
            currentMagazineAmmo += ammoDelta;
        }
        else if (totalAmmo < ammoDelta)
        {
            currentMagazineAmmo += totalAmmo;
            totalAmmo = 0;  
        }
        WeaponSystem.Instance.SetAmmo(ammoType,totalAmmo);
    }

    public int GetCurrentMagazineAmmo()
    {
        return currentMagazineAmmo;
    }

    public void Reset()
    {
        currentMagazineAmmo = magazineSize;
    }
}
