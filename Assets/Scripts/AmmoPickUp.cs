using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int ammoAmount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.tag == "Player")
        {
            foreach(Weapon weapon in collider.GetComponent<WeaponSystem>().inventory)
            {
                if (weapon is MissileWeapon)
                {
                    ((MissileWeapon)weapon).IncreaseAmmo(ammoAmount);
                }
            }
                
            Destroy(gameObject);
        }
    }
}
