using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : Interactable
{
    public GameObject weapon;
    public Transform weaponHolder;

    protected override void ExecuteInteractAction()
    {
        //switch weapons
        GameObject currentPlayerWeapon = GameController.Instance.player.GetComponent<WeaponSystem>().ChangeMissileWeapon(weapon);
        currentPlayerWeapon.transform.parent = weaponHolder.transform;
        currentPlayerWeapon.transform.position = weaponHolder.transform.position;
        currentPlayerWeapon.transform.localRotation = Quaternion.Euler(90f, 0, 0);
        currentPlayerWeapon.SetActive(true);
        weapon = currentPlayerWeapon;
        // weapon.transform.position = Player....
    }
}
