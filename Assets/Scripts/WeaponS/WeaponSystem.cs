using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    public Weapon[] inventory; //will be set up in inspector
    Weapon currentSelectedWeapon;
    public Text ammoDisplay;

    public Transform weaponHolder;


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

        if (Input.GetKeyDown(KeyCode.R)) StartReload();

        if (currentSelectedWeapon is MissileWeapon)
        {
            MissileWeapon selectedRifle = currentSelectedWeapon as MissileWeapon;
            ammoDisplay.text = selectedRifle.GetCurrentMagazineAmmo() + "/" + selectedRifle.GetTotalAmmo();
         }

    }

    void ChangeWeapon(int inventorySlot)
    {
        animator.SetTrigger("changeWeapon");
        animator.SetBool("reloading", false);

        if (currentSelectedWeapon != null)
        {
            currentSelectedWeapon.gameObject.SetActive(false);
        }

        currentSelectedWeapon = inventory[inventorySlot];

        if (currentSelectedWeapon != null)
        {
            currentSelectedWeapon.gameObject.SetActive(true);
            if(currentSelectedWeapon is MissileWeapon)
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
            (currentSelectedWeapon as Sword).StartCutting();
        }


        else if (currentSelectedWeapon is MissileWeapon)
        {
            MissileWeapon currentSelectedRifle = currentSelectedWeapon as MissileWeapon;
            currentSelectedRifle.Shoot();
        }
    }

    void StartReload()
    {
        if (currentSelectedWeapon is MissileWeapon)
        {
            if((currentSelectedWeapon as MissileWeapon).GetTotalAmmo()>0)
            animator.SetBool("reloading", true);
            StartCoroutine("ReloadingCoroutine");
        }
    }

    IEnumerator ReloadingCoroutine()
    {
        float reloadingTime = (currentSelectedWeapon as MissileWeapon).reloadTime;
        yield return new WaitForSeconds((reloadingTime));
        if (currentSelectedWeapon is MissileWeapon)
        {
            (currentSelectedWeapon as MissileWeapon).Reload();
        }
        animator.SetBool("reloading", false);
    }

    //called after gameOver to reset ammo
    public void Reset()
    {
        foreach(Weapon weapon in inventory)
        {
            if (weapon is MissileWeapon) (weapon as MissileWeapon).Reset();
        }
    }

    //returns currentWeapon
    public GameObject ChangeMissileWeapon(GameObject newWeapon)
    {
        GameObject currentWeapon = inventory[0].gameObject;
        inventory[0] = newWeapon.GetComponent<Weapon>();
        newWeapon.transform.parent = weaponHolder.transform;
        newWeapon.transform.position = weaponHolder.transform.position;
        newWeapon.transform.localRotation = Quaternion.Euler(90f, 0, 0);
        if (currentSelectedWeapon == currentWeapon.GetComponent<Weapon>()) currentSelectedWeapon = inventory[0];

        return currentWeapon;
    }
}
