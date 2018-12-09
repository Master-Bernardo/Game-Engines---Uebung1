using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    [SerializeField]
    protected Transform shootPoint; //point from which the projectiles are being shot
    protected int currentMagazineAmmo;
    private LineRenderer lr;
    public ParticleSystem ps;
    public float laserRange;
    public float fireRate;
    public float reloadTime;
    public int magazineSize;
    public GameObject sparks;

    private float fireRateTimer;

    public AmmoType ammoType;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        Reset();
        fireRateTimer = fireRate;
    }

    private void Update()
    {
        fireRateTimer -= Time.deltaTime;
        if (isEquipped && Input.GetMouseButton(0) && fireRateTimer <= 0 && currentMagazineAmmo > 0)
        {
            Shoot();
            fireRateTimer = fireRate;
            currentMagazineAmmo--;
        }
        else
        {
            lr.enabled = false;
            ps.gameObject.SetActive(false);
        }
    }

    public virtual void Shoot()
    {
        lr.enabled = true;
        lr.SetPosition(0, shootPoint.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, laserRange))
        {
            lr.SetPosition(1, hit.point);
            ps.startLifetime = laserRange / ps.startSpeed;

            ps.gameObject.SetActive(true);
            

            Instantiate(sparks, hit.point, transform.rotation);
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Health>().TakeDamage(damage);
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            lr.SetPosition(1, transform.position - transform.forward * laserRange);
            
        }


        
    }

    public virtual void Reload()
    {
        int totalAmmo = WeaponSystem.Instance.GetAmmo(ammoType);
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
        WeaponSystem.Instance.SetAmmo(ammoType, totalAmmo);
    }

    public int GetCurrentMagazineAmmo()
    {
        return currentMagazineAmmo;
    }

    public void Reset()
    {
        currentMagazineAmmo = magazineSize;
    }
}


