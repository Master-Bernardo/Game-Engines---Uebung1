using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Unit
{
    public float accuracy;
    public float shootingRange;
    public float shootingIntervall;
    float nextShootingTime;
    public Transform tower;
    public Transform barrel;
    public ProjectileWeapon rocketLauncher;

    bool reloading = false;

    protected override void Start()
    {
        base.Start();

        nextShootingTime = Time.time + shootingIntervall;
    }

    protected override void Update()
    {
        base.Update();

        if (alive)
        {
            if (target)
            {
                Vector3 turnTower = (target.position - tower.position);
                //tower.forward = turnTower;
                tower.forward = new Vector3(turnTower.x,0f,turnTower.z); // always look at player
                Vector3 turnBarrel = (target.position - barrel.position);
                barrel.forward = turnBarrel;


                //derGegner flieht immer auf 0.75% seiner shooting range und schießt, wenn der Spieler ausserhalb seiner Shooting Range ist, verfolgt er ihn

               if ((target.position - transform.position).magnitude > shootingRange)
               {
                    agent.SetDestination(target.position);
               }
               else
               {
                    if (Time.time >= nextShootingTime)
                    {

                        if (rocketLauncher.GetCurrentMagazineAmmo() > 0)
                        {
                            Shoot();
                            nextShootingTime = Time.time + shootingIntervall;
                        }
                        else
                        {
                            Reload();
                        }
                    }
               }
            }
        }
    }

    protected virtual void Shoot()
    {
        rocketLauncher.gameObject.transform.forward = Quaternion.Euler(Random.Range(0, accuracy), Random.Range(0, accuracy), Random.Range(0, accuracy)) * rocketLauncher.gameObject.transform.forward;
        rocketLauncher.Shoot();
    }

    protected virtual void Reload()
    {
        if (!reloading)
        {
            StartCoroutine("ReloadingCoroutine");
            reloading = true;
        }
    }

    IEnumerator ReloadingCoroutine()
    {
        float reloadingTime = rocketLauncher.reloadTime;
        yield return new WaitForSeconds((reloadingTime));
        rocketLauncher.Reset();
        reloading = false;
    }
}
