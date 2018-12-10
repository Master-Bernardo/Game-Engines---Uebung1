using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : ProjectileWeapon
{
    [Tooltip("How many bullets fit in one shell?")]
    public int shellSize = 5;

    public override void Shoot()
    {
        if (currentMagazineAmmo > 0)
        {
            for (int i = 0; i < shellSize; i++)
            {
                ShootOneBullet();
            }
            currentMagazineAmmo -= 1;
        }
    }

    private void ShootOneBullet()
    {
        Projectile bullet = Instantiate(projectile, shootPoint.position, Quaternion.Euler(
               shootPoint.eulerAngles.x + Random.Range(-bloom, bloom),
               shootPoint.eulerAngles.y + Random.Range(-bloom, bloom),
               shootPoint.eulerAngles.z)).GetComponent<Projectile>();
        bullet.damage = damage;
        bullet.team = Team.Friendly;
    }


}
