using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MissileWeapon
{
    [SerializeField]
    GameObject projectilePrefab;

    public override void Shoot()
    {
        if (currentMagazineAmmo > 0)
        {
            Bullet bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation).GetComponent<Bullet>();
            bullet.damage = damage;
            bullet.team = Team.Friendly;
            currentMagazineAmmo -= 1;
        }
    }
}
