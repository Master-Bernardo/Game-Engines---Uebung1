using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    Transform shootPoint; //point from which the projectiles are being shot

    public float reloadTime;
    public int magazineSize;
    private int currentMagazineAmmo;
    public int startAmmo;
    private int totalAmmo;

    private void Start()
    {
        Reset();
    }


    public void Shoot()
    {
        if (currentMagazineAmmo > 0)
        {
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            currentMagazineAmmo -= 1;
        }
    }

    public void Reload()
    {
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
    }

    public void IncreaseAmmo(int ammoAmount)
    {
        totalAmmo += ammoAmount;
    }

    public int GetCurrentMagazineAmmo()
    {
        return currentMagazineAmmo;
    }

    public int GetTotalAmmo()
    {
        return totalAmmo;
    }

    public void Reset()
    {
        totalAmmo = startAmmo;
        currentMagazineAmmo = magazineSize;
    }
}
