using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscapeShooterEnemy : Enemy
{
    [Header("KeepingDistance")]

    [Header("Shooting")]
    public float attackDamage;
    public float shootingRange; //max distance which we flee from player
    [Tooltip("how accurate is this bastard? - the smaller the better")]
    public float accuracy; // 
    public float shootingIntervall;
    float nextShootingTime;
    public GameObject projectilePrefab;
    [Tooltip("from where does the projectile launch?")]
    public Transform shootingPoint;
    public Transform gun; 

    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        nextShootingTime = Time.time + shootingIntervall;
    }

    public override void Update()
    {
        base.Update();
        if (alive)
        {
            if(target)
            { 
                transform.forward = (target.position - transform.position); // always look at player
                gun.forward = ((target.position) - gun.position);

                //derGegner flieht immer auf 0.75% seiner shooting range und schießt, wenn der Spieler ausserhalb seiner Shooting Range ist, verfolgt er ihn
                if ((target.position - transform.position).magnitude < 0.75 * shootingRange)
                {
                    agent.isStopped = false;
                    Vector3 escapePoint = transform.position - (target.position - transform.position);
                    agent.SetDestination(escapePoint);
                    if (Time.time >= nextShootingTime)
                    {

                        Shoot();
                        nextShootingTime = Time.time + shootingIntervall;
                    }
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

                    if (Time.time >= nextShootingTime)
                    {

                        Shoot();
                        nextShootingTime = Time.time + shootingIntervall;
                    }
                }
            }
        }
        else
        {
            gun.gameObject.SetActive(false);
        }
    }

    //Shoots aprojectile at player
    void Shoot()
    {
        Bullet projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation * Quaternion.Euler(Random.Range(0, accuracy), Random.Range(0, accuracy), Random.Range(0, accuracy))).GetComponent<Bullet>();
        if (!friendly) projectile.enemyBullet = true;
        else projectile.enemyBullet = false;
        projectile.damage = attackDamage;
        projectile.startSpeed = 40;
    }
}
