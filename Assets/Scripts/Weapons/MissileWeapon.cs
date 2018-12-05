using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileWeapon : Weapon
{
    
    [SerializeField]
    protected Transform shootPoint; //point from which the projectiles are being shot

    public float reloadTime;
    public int magazineSize;
    protected int currentMagazineAmmo;
    public int startAmmo;
    protected int totalAmmo;

    private void Start()
    {
        Reset();
    }


    public virtual void Shoot()
    {
        Debug.Log("piu piu");
    }

    public virtual void Reload()
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
