using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    Transform shootPoint; //point from which the projectiles are being shot

    public void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }
}
