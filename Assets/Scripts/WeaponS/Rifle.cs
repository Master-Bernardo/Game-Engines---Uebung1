using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : ProjectileWeapon
{
    

    public override void Shoot()
    {
        if (currentMagazineAmmo > 0)
        {
            Projectile bullet = Instantiate(projectile, shootPoint.position, Quaternion.Euler(
                shootPoint.eulerAngles.x + Random.Range(-bloom, bloom), 
                shootPoint.eulerAngles.y + Random.Range(-bloom, bloom), 
                shootPoint.eulerAngles.z)).GetComponent<Projectile>();
            bullet.damage = damage;
            bullet.team = team;
            currentMagazineAmmo -= 1;
        }
    }
}
