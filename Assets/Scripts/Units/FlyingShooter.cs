using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShooter : Unit
{

    [Header("FlyingShooter")]

    public float attackDamage;
    public float shootingRange; //max distance which we flee from player
    [Tooltip("how accurate is this bastard? - the smaller the better")]
    public float accuracy;

    public float shootingIntervall;
    float nextShootingTime;
    bool reloading = false;

    public ProjectileWeapon smg1;
    public ProjectileWeapon smg2;

    protected override void Start()
    {
        base.Start();
        agent.updateRotation = false;
        smg1.damage = attackDamage;
        smg2.damage = attackDamage;
        smg1.team = health.team;
        smg2.team = health.team;
    }

    protected override void Update()
    {
        base.Update();

        if (alive)
        {
            if (target)
            {
                agent.updateRotation = false;
                transform.forward = (target.position - transform.position); // always look at player
                smg1.gameObject.transform.forward = -((target.position) - smg1.gameObject.transform.position);
                smg2.gameObject.transform.forward = -((target.position) - smg2.gameObject.transform.position);

                //derGegner flieht immer auf 0.75% seiner shooting range und schießt, wenn der Spieler ausserhalb seiner Shooting Range ist, verfolgt er ihn
                if ((target.position - transform.position).magnitude < 0.75 * shootingRange)
                {
                    agent.isStopped = false;
                    Vector3 escapePoint = transform.position - (target.position - transform.position);
                    agent.SetDestination(escapePoint);

                    if (smg1.GetCurrentMagazineAmmo() > 0 && smg2.GetCurrentMagazineAmmo() > 0)
                    {
                        if (Time.time >= nextShootingTime)
                        {

                            Shoot();
                            nextShootingTime = Time.time + shootingIntervall;
                        }
                    }
                    else Reload();
                }
                else if ((target.position - transform.position).magnitude > shootingRange)
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                }
                else
                {
                    //wenn er im perfektem Abstand zum Spieler ist, schießt er
                    agent.isStopped = true;
                    if (smg1.GetCurrentMagazineAmmo() > 0 && smg2.GetCurrentMagazineAmmo() > 0)
                    {
                        if (Time.time >= nextShootingTime)
                        {

                            Shoot();
                            nextShootingTime = Time.time + shootingIntervall;
                        }
                    }
                    else Reload();
                }
            }
        }
        else
        {

        }
    }

    //Shoots aprojectile at player
    protected virtual void Shoot()
    {
        smg1.gameObject.transform.forward =  Quaternion.Euler(Random.Range(0, accuracy), Random.Range(0, accuracy), Random.Range(0, accuracy)) * smg1.gameObject.transform.forward;
        smg2.gameObject.transform.forward =  Quaternion.Euler(Random.Range(0, accuracy), Random.Range(0, accuracy), Random.Range(0, accuracy)) * smg2.gameObject.transform.forward;
        smg1.Shoot();
        smg2.Shoot();
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
        float reloadingTime = smg1.reloadTime;
        yield return new WaitForSeconds((reloadingTime));
        smg1.Reset();
        smg2.Reset();
        reloading = false;
    }


}
