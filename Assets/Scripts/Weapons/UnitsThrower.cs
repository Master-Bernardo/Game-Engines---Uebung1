using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitsThrower : ProjectileWeapon
{
    [SerializeField]
    GameObject unitPrefab;

    public override void Shoot()
    {
        if (currentMagazineAmmo > 0)
        {
            GameObject unit  = Instantiate(unitPrefab, shootPoint.position, shootPoint.rotation);

            currentMagazineAmmo -= 1;
        }
    }
}
