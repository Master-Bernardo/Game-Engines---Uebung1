using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    Weapon[] inventory; //will be set up in inspector
    Weapon currentSelectedWeapon;

    //for animation
    public Animator animator;

    void Start()
    {
        foreach (Weapon weapon in inventory)
        {
            if(weapon!=null)weapon.gameObject.SetActive(false);
        }
        ChangeWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftMouseButton();
        }

    }

    void ChangeWeapon(int inventorySlot)
    {
        animator.SetTrigger("changeWeapon");

        if (currentSelectedWeapon != null)
        {
            currentSelectedWeapon.gameObject.SetActive(false);
        }

        currentSelectedWeapon = inventory[inventorySlot];

        if (currentSelectedWeapon != null)
        {
            currentSelectedWeapon.gameObject.SetActive(true);
            if(currentSelectedWeapon is Rifle)
            {
                animator.SetBool("rifleSelected", true);
                animator.SetBool("swordSelected", false);
                animator.SetBool("nothingSelected", false);
            }
            else if (currentSelectedWeapon is Sword)
            {
                animator.SetBool("rifleSelected", false);
                animator.SetBool("swordSelected", true);
                animator.SetBool("nothingSelected", false);
            }
        }
        else
        {
            animator.SetBool("rifleSelected", false);
            animator.SetBool("swordSelected", false);
            animator.SetBool("nothingSelected", true);
        }
    }

    public void HandleLeftMouseButton()
    {
        //wenn wir nur unsere Hände selected haben -> faustschlag
        if (currentSelectedWeapon == null)
        {
            animator.SetTrigger("fistAttack");
        }
        else if(currentSelectedWeapon is Sword)
        {
            animator.SetTrigger("swordAttack");
        }


        else if (currentSelectedWeapon is Rifle)
        {
            Rifle currentSelectedRifle = currentSelectedWeapon as Rifle;
            currentSelectedRifle.Shoot();
        }
    }
}
