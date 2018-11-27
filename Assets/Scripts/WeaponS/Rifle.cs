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
    public float magazineSize;
    private float currentMagazineAmmo;

    private void Start()
    {
        currentMagazineAmmo = magazineSize;
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
        currentMagazineAmmo = magazineSize;
    }
}
